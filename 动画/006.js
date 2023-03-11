
// 绘制文本
function drawText(comp, options) {
    var comp = comp;
    var myTextLayer = comp.layers.addText();
    var textProp = myTextLayer.property("Source Text");
    var textDocument = textProp.value;
    textDocument.resetCharStyle();
    textDocument.fontSize = options.fontSize;
    textDocument.fillColor = options.color;
    textDocument.font = options.font || "PingFang-SC-Bold";
    textDocument.applyFill = true;
    if (options.tracking)
        textDocument.tracking = options.tracking;



    textDocument.text = options.text;
    textDocument.justification = options.justification || ParagraphJustification.CENTER_JUSTIFY;
    textProp.setValue(textDocument);
    textProp.setValue(textDocument);
    // https://ae-scripting.docsforadobe.dev/other/textdocument.html#textdocument-baselinelocs
    myTextLayer.inPoint = options.inPoint;
    myTextLayer.outPoint = options.outPoint;
    /*
var inPoint = 0;
// FZLanTingHeiS-DB1-GBK
var textLayer = drawText({
    "color": [0 / 1 / 255, 0 / 1 / 255, 0 / 1 / 255],
    "font": "",
    "fontSize": 72,
    "inPoint": inPoint,
    "justification": ParagraphJustification.CENTER_JUSTIFY,
    "outPoint": 1,
    "text": "你好你好你好你好你好"
});
textLayer.position.setValue(
    [textLayer.position.value[0],
        1186]
)
var textLayerRect = textLayer.sourceRectAtTime(inPoint, false);
$.writeln(Math.round(textLayerRect.width) + 'X' + Math.round(textLayerRect.height));

*/
    return myTextLayer;
}
function step1(){
   var layer=app.project.activeItem.selectedLayers[0];
   layer.scale.setValueAtTime(0,[0,0]);
   layer.scale.setValueAtTime(.35,[185,185]);
   layer.position.setValueAtTime(0.5,[
    layer.position.value[0],layer.position.value[1]]);
   layer.position.setValueAtTime(.85,[320,layer.position.value[1]]);
}

function step2(){
    var textLayer = drawText(
        app.project.activeItem
        ,{
        "color": [255 / 1 / 255, 255 / 1 / 255, 255 / 1 / 255],
        "font": "",
        "fontSize": 60,
        "inPoint": 0,
        "justification": ParagraphJustification.CENTER_JUSTIFY,
        "outPoint": 10,
        "text": "污点修复画笔工具"
    });
    var textLayerRect = textLayer.sourceRectAtTime(0, false);
$.writeln(Math.round(textLayerRect.width) + 'X' + Math.round(textLayerRect.height));
textLayer.position.setValueAtTime(1,
    [textLayer.position.value[0],
    386+57]
)
textLayer.position.setValueAtTime(1.35,
    [textLayer.position.value[0],
    382]
)
textLayer.opacity.setValueAtTime(1,
    0
)
textLayer.opacity.setValueAtTime(1.35,
   100
)
}




function step2(){
    var textLayer = drawText(
        app.project.activeItem
        ,{
        "color": [255 / 1 / 255, 255 / 1 / 255, 255 / 1 / 255],
        "font": "",
        "fontSize": 60,
        "inPoint": 0,
        "justification": ParagraphJustification.CENTER_JUSTIFY,
        "outPoint": 10,
        "text": "快捷键 J"
    });
    var textLayerRect = textLayer.sourceRectAtTime(0, false);
$.writeln(Math.round(textLayerRect.width) + 'X' + Math.round(textLayerRect.height));
textLayer.position.setValueAtTime(1.35,
    [517,
    386+57+86]
)
textLayer.position.setValueAtTime(1.85,
    [517,
    382+86]
)
textLayer.opacity.setValueAtTime(1.35,
    0
)
textLayer.opacity.setValueAtTime(1.85,
   100
)
}

// step1();
//step2();

step2();