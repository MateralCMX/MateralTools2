/// <reference path="../m-tools/m-tools.ts" />
'use strict';
MateralTools.DOMManager.AddEvent(window, "load", function () {
    var ClientInfoM = new MateralTools.ClientInfoModel();
    //是IE时执行
    if (ClientInfoM.BrowserInfoM.IE) {
        var IEVersion = parseFloat(ClientInfoM.BrowserInfoM.Version);
        //IE版本小于等于IE8
        if (IEVersion <= 8) {
            if (!MateralTools.ToolManager.IsNullOrUndefined(document.createElement)) {
                document.createElement("header");
                document.createElement("main");
                document.createElement("nav");
                document.createElement("section");
                document.createElement("article");
                document.createElement("footer");
                document.createElement("video");
            }
        }
    }
});
//# sourceMappingURL=m-Reset.js.map