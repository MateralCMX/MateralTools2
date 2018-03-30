using System.Drawing;
using System.Drawing.Imaging;

namespace MateralTools.MImage
{
    /// <summary>
    /// 图片管理类
    /// </summary>
    public class ImageManager
    {
        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        public static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            PixelFormat[] indexedPixelFormats = {
                PixelFormat.Undefined,
                PixelFormat.DontCare,
                PixelFormat.Format16bppArgb1555,
                PixelFormat.Format1bppIndexed,
                PixelFormat.Format4bppIndexed,
                PixelFormat.Format8bppIndexed
            };
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 带有索引像素格式的图像转换为位图
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns></returns>
        public static Bitmap PixeIFormatConvertBitMap(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(img, 0, 0);
            }
            return bmp;
        }
        /// <summary>
        /// 带有索引像素格式的图像转换为位图
        /// </summary>
        /// <param name="imgPatch">图片地址</param>
        /// <returns></returns>
        public static Bitmap PixeIFormatConvertBitMap(string imgPatch)
        {
            Image img = Image.FromFile(imgPatch);
            return PixeIFormatConvertBitMap(img);
        }
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="img">要添加水印的图片</param>
        /// <param name="waterMarkImg">水印图片</param>
        /// <param name="waterPosition">水印图片位置</param>
        /// <returns>添加过水印的图片</returns>
        public static Bitmap AddWaterMark(Bitmap img, Bitmap waterMarkImg, Point waterPosition)
        {
            Graphics graphics = Graphics.FromImage(img);
            int width = waterMarkImg.Width;
            int height = waterMarkImg.Height;
            graphics.DrawImage(waterMarkImg, new Rectangle(waterPosition.X, waterPosition.Y, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
            return img;
        }
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="imgPath">要添加水印的图片地址</param>
        /// <param name="waterMarkImg">水印图片</param>
        /// <param name="waterPosition">水印图片位置</param>
        /// <returns>添加过水印的图片</returns>
        public static Bitmap AddWaterMark(string imgPath, Bitmap waterMarkImg, Point waterPosition)
        {
            Image img = Image.FromFile(imgPath);
            Bitmap bitImg;
            if (IsPixelFormatIndexed(img.PixelFormat))
            {
                bitImg = PixeIFormatConvertBitMap(img);
            }
            else
            {
                bitImg = (Bitmap)img;
            }
            return AddWaterMark(bitImg, waterMarkImg, waterPosition);
        }
        /// <summary>
        /// 根据水印文字获得水印图片
        /// </summary>
        /// <param name="waterMarkStr">水印文字</param>
        /// <param name="waterSize">水印图片大小</param>
        /// <returns>水印图片</returns>
        public static Bitmap GetWaterMarkImageByStr(string waterMarkStr, Size waterSize)
        {
            Bitmap img = new Bitmap(waterSize.Width, waterSize.Height);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, new Rectangle() { X = 0, Y = 0, Height = waterSize.Height, Width = waterSize.Width });
            Font font = new Font("宋体", 10);
            g.DrawString(waterMarkStr, font, Brushes.Black, new PointF() { X = 0, Y = 0 });
            return img;
        }
    }
}
