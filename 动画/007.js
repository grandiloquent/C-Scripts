function parseSeconds(time, fps) {
    // 0:01:02:06
    var matched = /(\d{1}):(\d{2}):(\d{2}):(\d{2})/.exec(time);
    var seconds = parseInt(matched[1]) * 3600
        + parseInt(matched[2]) * 60
        + parseInt(matched[3])
        + parseInt(matched[4]) * 1 / fps
    $.writeln(seconds)
    return seconds;
}
function parseLine(s) {
    var pieces = s.split('|');
    var start = parseSeconds(pieces[0], 20);
    var end = parseSeconds(pieces[1], 20);
    var x = parseInt(pieces[2]);
    var y = parseInt(pieces[3]);
    var name = pieces[4];
    return [start, end, x, y, name]
}
function scale(inPoint, outPoint, x, y, layer) {
    var comp = app.project.activeItem;
    var times = [
        inPoint,
        inPoint + .5,
        outPoint,
        outPoint + .5,
    ]

    layer.scale.setValueAtTime(times[0], [100, 100]);
    layer.scale.setValueAtTime(times[1], [200, 200]);
    layer.scale.setValueAtTime(times[2], [200, 200]);
    layer.scale.setValueAtTime(times[3], [100, 100]);


    layer.position.setValueAtTime(times[0], [640, 360]);

    var halfX = comp.width / 2;
    var halfY = comp.height / 2;
    var transformedX = halfX - halfX * 1 - (x - (comp.width - x) * 1 - halfX);
    var transformedY = halfY - halfY * 1 - (y - (comp.height - y) * 1 - halfY);

    layer.position.setValueAtTime(times[1], [
        transformedX,
        transformedY
    ])
    layer.position.setValueAtTime(times[2], [
        transformedX,
        transformedY
    ])
    layer.position.setValueAtTime(times[3], [640, 360]);
    // setScaleEase(layer, 1);
    // setScaleEase(layer, 2);
    // setScaleEase(layer, 3);
    // setScaleEase(layer, 4);
    // setPositionEase(layer, 1);
    // setPositionEase(layer, 2);
    // setPositionEase(layer, 3);
    // setPositionEase(layer, 4);

}

function findLayer(name) {
    var comp = app.project.activeItem;

    for (var i = 0; i < comp.numLayers; i++) {
        if (name === comp.layer(i + 1).name) {
            return comp.layer(i + 1);
        }
    }
    return null;
}
function setScaleEase(layer, index) {
    var vi = new KeyframeEase(0, 75);
    var vo = new KeyframeEase(0, 33.333333);
    layer.scale.setTemporalEaseAtKey(index, [
        vi, vi, vo
    ], [vi, vi, vo]);
    layer.scale.setInterpolationTypeAtKey(index, 6613)
}
function setPositionEase(layer, index) {
    var vi = new KeyframeEase(0, 75);
    layer.position.setTemporalEaseAtKey(index, [
        vi
    ], [vi]);
    layer.position.setInterpolationTypeAtKey(index, 6613)
}
function drawEllipse(comp, options) { /*
    drawEllipse({
        "color": [1,1,1],
        "height": 100,
        "opacity": 100,
        "width": 100
    })
    */
    var layer = comp.layers.addShape();
    var shapeGroup = layer.property("Contents").addProperty("ADBE Vector Group");
    var shapeGroupContents = shapeGroup.property("Contents");
    var myRect = shapeGroupContents.addProperty("ADBE Vector Shape - Ellipse");
    var rectSize = myRect.property("ADBE Vector Ellipse Size");
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
    return layer;
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

function anchorLeft(square, width) {
    // 锚点默认位于对象中心，所以如果左移宽度的一半，则可以将锚点移到左端
    square.anchorPoint.setValue([-width / 2, 0, 0]);
    var position = square.position.value;
    // 因为锚点左移，位置的X轴也应等量左移，以保持对象原来的位置
    square.position.setValue([position[0] - width / 2, position[1], position[2]])
}
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
function createTypewriterEffect(layer, startTime, endTme) {
    var animators = layer.Text.Animators;
    var animator = animators.addProperty("ADBE Text Animator");
    animator.property("ADBE Text Animator Properties").addProperty("ADBE Text Opacity").setValue(0);
    var start = animator.property("ADBE Text Selectors").addProperty("ADBE Text Selector").property("ADBE Text Percent Start")
    start.setValueAtTime(startTime, 0);
    start.setValueAtTime(startTime + .45, 100);
    start.setValueAtTime(endTme - .45, 100);
    start.setValueAtTime(endTme - .15, 0);

}

function build(start, text, name) {
    var comp = app.project.items.addComp(name, app.project.activeItem.width, app.project.activeItem.height, app.project.activeItem.pixelAspect, app.project.activeItem.duration, app.project.activeItem.frameRate);

    var l = app.project.activeItem.layers.add(comp)
    l.startTime = start;
    l.position.setValue([90, 560]);
    var color = [253 / 1 / 255, 97 / 1 / 255, 0 / 1 / 255];
    var duration = 1;
    var yPosition = 300;
    var height = 90;
    var distance = 317;
    var inPoint = 0;
    var outPoint = 3;
    var textLayer = drawText(comp, {
        "color": [255 / 1 / 255, 255 / 1 / 255, 255 / 1 / 255],
        "font": "",
        "fontSize": 32,
        "inPoint": inPoint + .45,
        "justification": ParagraphJustification.CENTER_JUSTIFY,
        "outPoint": outPoint - .45,
        "text": text
    });
    var textLayerRect = textLayer.sourceRectAtTime(inPoint, false);
    $.writeln(Math.round(textLayerRect.width) + 'X' + Math.round(textLayerRect.height));
    textLayer.position.setValue([
        textLayer.position.value[0],
        textLayer.position.value[1] + 11.6
    ]);
    distance = Math.round(textLayerRect.width);
    var color1 = drawEllipse(comp, {
        "color": color,
        "height": height,
        "opacity": 100,
        "width": height
    });
    var color2 = drawEllipse(comp, {
        "color": color,
        "height": height,
        "opacity": 100,
        "width": height
    });
    var square = drawSquare(comp, {
        "color": color,
        "height": height,
        "opacity": 100,
        "width": distance
    });
    anchorLeft(square, distance);
    square.position.setValue([
        color1.position.value[0],
        square.position.value[1]
    ]);
    color1.inPoint = inPoint;
    color1.outPoint = outPoint;
    square.inPoint = inPoint + .35;
    square.outPoint = outPoint - .35;
    color2.inPoint = inPoint + .35;
    color2.outPoint = outPoint - .35;

    color1.scale.setValueAtTime(inPoint, [0, 0]);
    color1.scale.setValueAtTime(inPoint + .25, [100, 100]);
    color1.scale.setValueAtTime(outPoint - .25, [100, 100]);
    color1.scale.setValueAtTime(outPoint, [0, 0]);
    var x = color2.position.value[0];
    color2.position.setValueAtTime(inPoint + .35, [x, color2.position.value[1]]);
    color2.position.setValueAtTime(inPoint + 1, [x + distance, color2.position.value[1]]);
    color2.position.setValueAtTime(outPoint - 1, [x + distance, color2.position.value[1]]);
    color2.position.setValueAtTime(outPoint - .35, [x, color2.position.value[1]]);

    square.scale.setValueAtTime(inPoint + .35, [0, 100]);
    square.scale.setValueAtTime(inPoint + 1, [100, 100]);
    square.scale.setValueAtTime(outPoint - 1, [100, 100]);
    square.scale.setValueAtTime(outPoint - .35, [0, 100]);

    setPositionEase(color2, 1);
    setPositionEase(color2, 2);
    setPositionEase(color2, 3);
    setPositionEase(color2, 4);
    setScaleEase(color1, 1);
    setScaleEase(color1, 2);
    setScaleEase(color1, 3);
    setScaleEase(color1, 4);
    setScaleEase(square, 1);
    setScaleEase(square, 2);
    setScaleEase(square, 3);
    setScaleEase(square, 4);



    textLayer.anchorPoint.setValue([
        square.anchorPoint.value[0],
        square.anchorPoint.value[1],
    ])
    createTypewriterEffect(textLayer, textLayer.inPoint, textLayer.outPoint)
    textLayer.moveToBeginning()

}


function setScale() {
    var fileObj = new File("C:/Users/Administrator/Desktop/代码/脚本/动画/1.txt");
    fileObj.open("r");
    var array = [];
    do {
        array.push(parseLine(fileObj.readln()));
    } while (fileObj.tell() < fileObj.length)
    fileObj.close();

    for (var j = 0; j < array.length; j++) {
        scale(
            array[j][0],
            array[j][1],
            array[j][2],
            array[j][3],
            findLayer(array[j][4])
        )
    }
}

function setDuration() {
    var comp = app.project.activeItem;

    var layers = [];
    for (var i = 0; i < comp.numLayers; i++) {
        if (/^\d+\.mp3$/.test(comp.layer(i + 1).name)) {
            var layer = comp.layer(i + 1);
            layers.push(layer);


        }
    }
    layers = layers.sort(function (x, y) {
        return parseInt(x.name) - parseInt(y.name)
    })
    var start = 0;
    for (var i = 0; i < layers.length; i++) {
        layers[i].startTime = start;
        var layer = findLayer((i + 1).toString() + ".mp4");
        layer.startTime = start;
        start += layers[i].outPoint - layers[i].inPoint
        layer.outPoint = start;
        // $.writeln(findLayer((i+1).toString()).outPoint);

    }

}

function setShortKey() {
    var fileObj = new File("C:/Users/Administrator/Desktop/代码/脚本/动画/2.txt");
    fileObj.open("r");
    var array = [];
    do {
        array.push(fileObj.readln());
    } while (fileObj.tell() < fileObj.length)
    fileObj.close();
    for (var j = 0; j < array.length; j++) {
        var pieces=array[j].split('|')
        build(parseSeconds(pieces[0],20),pieces[1],j+''+j)
    }
    /*
    var comp = app.project.activeItem;

    var layers = [];
    for (var i = 0; i < comp.numLayers; i++) {
        if (/^\d+\.mp3$/.test(comp.layer(i + 1).name)) {
            var layer = comp.layer(i + 1);
            layers.push(layer);


        }
    }
    layers = layers.sort(function (x, y) {
        return parseInt(x.name) - parseInt(y.name)
    })
    var start = 0;
    var keys = [
        "",
        "Shift+Ctrl+N 新建图层",
        "J 修复画笔工具",
        "",
        "",
        "Z 缩放工具",
        "",
        "Alt+单击 定义修复源点",
    ]
    for (var i = 0; i < layers.length; i++) {
        if(keys[i].length>0){
            build(start,keys[i],i+''+i)
        }
        start += layers[i].outPoint - layers[i].inPoint
        layer.outPoint = start;

        // $.writeln(findLayer((i+1).toString()).outPoint);

    }
    */
}
//setDuration();
setScale();
//setShortKey();