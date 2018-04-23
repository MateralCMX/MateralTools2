using Com.Alipay.Domain;
using Com.Alipay.Model;
using Com.Alipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Alipay.Business;
using System.Threading;
using System.Drawing;
using MateralTools.MEncryption;

namespace MateralTools.MAlipay
{
    /// <summary>
    /// 支付宝面对面支付管理器
    /// </summary>
    public class F2FPayManager
    {
        /// <summary>
        /// 服务连接
        /// </summary>
        private IAlipayTradeService _serviceClient;
        /// <summary>
        /// 配置对象
        /// </summary>
        private AlipayConfigmModel _config;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM">支付宝配置对象</param>
        public F2FPayManager(AlipayConfigmModel configM)
        {
            _config = configM;
            _serviceClient = F2FBiz.CreateClientInstance(
                _config.ServerURL,
                _config.AppId,
                _config.MerchantPrivateKey,
                _config.Version,
                _config.SignType,
                _config.PublicKey,
                _config.CharSet
            );
        }
        /// <summary>
        /// 获得支付二维码
        /// </summary>
        /// <param name="aoM">支付宝支付订单对象</param>
        /// <returns>支付二维码</returns>
        public Bitmap GetPayQRCode(AlipayOrderModel aoM)
        {
            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(aoM);
            string out_trade_no = builder.out_trade_no;
            AlipayF2FPrecreateResult precreateResult = _serviceClient.tradePrecreate(builder, aoM.NotifyUrl);
            Bitmap img;
            if (precreateResult.Status == ResultEnum.SUCCESS)
            {
                img = EncryptionManager.QRCodeEncode(precreateResult.response.QrCode);
            }
            else if (precreateResult.Status == ResultEnum.FAILED)
            {
                throw new MAlipayException(precreateResult.response.Body);
            }
            else
            {
                if (precreateResult.response == null)
                {
                    throw new MAlipayException("配置或网络异常，请检查后重试");
                }
                else
                {
                    throw new MAlipayException("系统异常，请更新外部订单后重新发起请求");
                }
            }
            return img;
        }
        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <param name="aoM">支付宝支付订单对象</param>
        /// <returns>请求数据集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(AlipayOrderModel aoM)
        {
            //线上联调时，请输入真实的外部订单号。
            string out_trade_no = aoM.ID;//订单号
            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder
            {
                //收款账号
                seller_id = _config.PID,
                //订单编号
                out_trade_no = out_trade_no,
                //订单总金额
                total_amount = aoM.TotalPrice.ToString(),
                //参与优惠计算的金额
                discountable_amount = aoM.DiscounPrice.ToString(),
                //不参与优惠计算的金额
                undiscountable_amount = aoM.UnDiscounPrice.ToString(),
                //订单名称
                subject = aoM.Name,
                //自定义超时时间
                timeout_express = "5m",
                //订单描述
                body = aoM.Description,
                //门店编号，很重要的参数，可以用作之后的营销
                store_id = aoM.StoreID,
                //操作员编号，很重要的参数，可以用作之后的营销
                operator_id = aoM.OperatorId
            };
            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods;
            foreach (AlipayProductModel item in aoM.Items)
            {
                goods = new GoodsInfo
                {
                    goods_id = item.ID,
                    goods_name = item.Name,
                    price = item.Price.ToString(),
                    quantity = item.Nubmer.ToString()
                };
                gList.Add(goods);
            }
            builder.goods_detail = gList;
            //系统商接入可以填此参数用作返佣
            ExtendParams exParam = new ExtendParams
            {
                sys_service_provider_id = aoM.NotifyParams
            };
            builder.extend_params = exParam;
            return builder;

        }
    }
}
