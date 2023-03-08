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
function drawSquare(comp, options) { /*
    drawSquare({
        "color": [1,1,1],
        "height": 100,
        "opacity": 100,
        "radius": 8,
        "width": 100
    })
    */
    var layer = comp.layers.addShape();
    var shapeGroup = layer.property("Contents").addProperty("ADBE Vector Group");
    var shapeGroupContents = shapeGroup.property("Contents");
    var myRect = shapeGroupContents.addProperty("ADBE Vector Shape - Rect");
    var rectSize = myRect.property("ADBE Vector Rect Size");
    rectSize.setValue([options.width, options.height]);
    if (options.color) {
        if (options.strokeWidth) {
            var stroke = shapeGroup.property("Contents").addProperty("ADBE Vector Graphic - Stroke");
            stroke.property("Color").setValue(options.color);
            stroke.property("ADBE Vector Stroke Width").setValue(options.strokeWidth);
            stroke.property("Opacity").setValue(options.opacity);
        } else {
            var fill = shapeGroupContents.addProperty("ADBE Vector Graphic - Fill");
            fill.property("Color").setValue(options.color);
            fill.property("Opacity").setValue(options.opacity);
        }
    }
    shapeGroupContents.addProperty("ADBE Vector Filter - RC").property('ADBE Vector RoundCorner Radius').setValue(options.radius || 0);
    return layer;
}
function anchorTop(square, height) {
    // 锚点默认位于对象中心，所以如果左移宽度的一半，则可以将锚点移到左端
    square.anchorPoint.setValue([0, -height / 2, 0]);
    var position = square.position.value;
    // 因为锚点左移，位置的X轴也应等量左移，以保持对象原来的位置
    square.position.setValue([position[0], position[1] - height / 2, position[2]])
}

var inPoint = 0;
var comp = app.project.activeItem;
// [244 / 1 / 255, 67 / 1 / 255, 54 / 1 / 255]
var textLayer = drawText(comp, {
    "color": [255 / 1 / 255, 255 / 1 / 255, 255 / 1 / 255],
    "font": "",
    "fontSize": 72,
    "inPoint": inPoint,
    "justification": ParagraphJustification.CENTER_JUSTIFY,
    "outPoint": 5,
    "text": "Photoshop磨皮"
});



var textLayerRect = textLayer.sourceRectAtTime(inPoint, false);
$.writeln(Math.round(textLayerRect.width) + 'X' + Math.round(textLayerRect.height) + "-" + (64 / 2 + Math.round(textLayerRect.height)) / (Math.round(textLayerRect.height) + 64));
var square = drawSquare(comp, {
    "color": [244 / 1 / 255, 67 / 1 / 255, 54 / 1 / 255],
    "height": Math.round(textLayerRect.height) + 64,
    "opacity": 100,
    "radius": 8,
    "width": Math.round(textLayerRect.width) + 96
})
anchorTop(square, Math.round(textLayerRect.height) + 64)
square.moveAfter(textLayer);
/*
square.scale.setValueAtTime(0,[100,100]);
square.scale.setValueAtTime(1,[100,176]);
square.position.setValueAtTime(0,[
    square.position.value[0],
    square.position.value[1]
])
square.position.setValueAtTime(1,[
    square.position.value[0],
    square.position.value[1]-.76*(Math.round(textLayerRect.height)+64)/2
])
textLayer.position.setValueAtTime(0,[
    textLayer.position.value[0],
    textLayer.position.value[1]+371.3-348.7]
)
textLayer.position.setValueAtTime(1,[
    textLayer.position.value[0],
    textLayer.position.value[1]-.76*(Math.round(textLayerRect.height)+64)/2
]
)
*/

function titleStep1() {
    var x = textLayer.position.value[0];
    var y = textLayer.position.value[1];
    var start = 0;
    var end = .35;
    textLayer.position.setValueAtTime(start, [
        x - 100,
        y + 371.3 - 348.7]
    )
    textLayer.position.setValueAtTime(end, [
        x,
        y + 371.3 - 348.7]
    )
    textLayer.opacity.setValueAtTime(start, 0);
    textLayer.opacity.setValueAtTime(end, 100);

}
function squareStep1() {
    var start = 0;
    var end = .35;
    var x = square.position.value[0];
    var y = square.position.value[1]
    square.position.setValueAtTime(start, [
        x + 100,
        y
    ])
    square.position.setValueAtTime(end, [
        x,
        y
    ])
    square.opacity.setValueAtTime(start, 0);
    square.opacity.setValueAtTime(end, 100);
}

function titleStep2() {
    var x = textLayer.position.value[0];
    var y = textLayer.position.value[1];
    var start = .5;
    var end = .85;
    textLayer.position.setValueAtTime(start, [
        x,
        y]
    )
    textLayer.position.setValueAtTime(end, [
        x,
        y - .76 * (Math.round(textLayerRect.height) + 64) / 2
    ]
    )

}
function squareStep2() {
    var start = .5;
    var end = .85;
    var x = square.position.value[0];
    var y = square.position.value[1]
    square.position.setValueAtTime(start, [
        x + 100,
        y
    ])
    square.position.setValueAtTime(end, [
        x,
        y
    ])
    square.scale.setValueAtTime(start, [100, 100]);
    square.scale.setValueAtTime(end, [100, 176]);
    square.position.setValueAtTime(start, [
        x,
        y
    ])
    square.position.setValueAtTime(end, [
        x,
        y - .76 * (Math.round(textLayerRect.height) + 64) / 2
    ])
}
titleStep1();
titleStep2();
squareStep1();
squareStep2();
var textLayer2 = drawText(comp, {
    "color": [255 / 1 / 255, 255 / 1 / 255, 255 / 1 / 255],
    "font": "",
    "fontSize": 72,
    "inPoint": 1.5,
    "justification": ParagraphJustification.CENTER_JUSTIFY,
    "outPoint": 5,
    "text": "美化头像"
});
textLayer2.position.setValueAtTime(0,[
    textLayer2.position.value[0],
    textLayer2.position.value[1]+371.3-348.7+59]
)
function drawLine(vertices, color, width) {
    var comp = app.project.activeItem;
    var shapeLayer = comp.layers.addShape();
    var shapeGroup = shapeLayer.property("ADBE Root Vectors Group").addProperty("ADBE Vector Group");
    var sc = shapeGroup.property("Contents");
    var shapePath = sc.addProperty("ADBE Vector Shape - Group").property("ADBE Vector Shape");
    var shapePathData = new Shape();
    shapePathData.vertices = vertices;
    shapePathData.closed = false;
    shapePath.setValue(shapePathData);
    var stroke = sc.addProperty("ADBE Vector Graphic - Stroke");
    stroke.property("Color").setValue(color);
    stroke.property("ADBE Vector Stroke Width").setValue(width);
    // 圆角
    stroke.property('ADBE Vector Stroke Line Join').setValue(2);
    shapeLayer.property("Opacity").setValue(100);
    return shapeLayer;
}
var line=drawLine([
    [0,0],
    [501,0],
    [501,0],
    [0,0],
],[
    1,1,1
],6)

function lineStep1(){
    var x = line.position.value[0];
    var y = line.position.value[1];
    var start = 1;
    var end = 1.35;
line.anchorPoint.setValue([
    501/2,0
])
    line.position.setValue([
        line.position.value[0],
        line.position.value[1]
    ]);
    line.scale.setValueAtTime(start, [0, 100]);
    line.scale.setValueAtTime(end, [100, 100]);
}
lineStep1();
function subtitleStep1() {
    var x = textLayer2.position.value[0];
    var y = textLayer2.position.value[1];
    var start = 1.5;
    var end = 1.85;
    textLayer2.position.setValueAtTime(start, [
        x ,
        y +50]
    )
    textLayer2.position.setValueAtTime(end, [
        x,
        y]
    )
    textLayer2.opacity.setValueAtTime(start, 0);
    textLayer2.opacity.setValueAtTime(end, 100);

}
subtitleStep1();