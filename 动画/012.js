
function animateRoundedRectangle(comp, inPoint, outPoint, offset, size) {
    var layer = drawRoundedRectangle(comp, {
        "color": [37 * 1 / 255, 37 * 1 / 255, 37 * 1 / 255],
        "size": size,
        "opacity": 75,
        "offset": offset,
        duration: duration,
        "inPoint": inPoint,
        "outPoint": outPoint,
    })
    layer.position.setValue([
        layer.position.value[0],
        yPosition
    ]);
    return layer;
}
function addComp() {
    var items = app.project.items;
    var duration, pixelAspect, width, height, frameRate;
    for (var i = 0; i < items.length; i++) {
        if (items[i + 1].toString() === "[object CompItem]") {
            duration = items[i + 1].duration;
            pixelAspect = items[i + 1].pixelAspect;
            width = items[i + 1].width;
            height = items[i + 1].height;
            frameRate = items[i + 1].frameRate;
            break;
        }
    }
    return app.project.items.addComp("图标", width, height, pixelAspect, duration, frameRate);
}
function addMask(layer, inPoint, outPoint, textLayerRect) {
    var myShape1 = new Shape();
    myShape1.vertices = [
        [textLayerRect.left + textLayerRect.width / 2, textLayerRect.top],
        [textLayerRect.left + textLayerRect.width - textLayerRect.width / 2, textLayerRect.top],
        [textLayerRect.left + textLayerRect.width - textLayerRect.width / 2, textLayerRect.top + textLayerRect.height],
        [textLayerRect.left + textLayerRect.width / 2, textLayerRect.top + textLayerRect.height]
    ]
    myShape1.closed = true;
    var myShape2 = new Shape();
    myShape2.vertices = [
        [textLayerRect.left, textLayerRect.top],
        [textLayerRect.left + textLayerRect.width, textLayerRect.top],
        [textLayerRect.left + textLayerRect.width, textLayerRect.top + textLayerRect.height],
        [textLayerRect.left, textLayerRect.top + textLayerRect.height]
    ]
    myShape2.closed = true;
    var myMask = layer.Masks.addProperty("Mask")
    myMask.property("maskShape").setValueAtTime(inPoint, myShape1);
    myMask.property("maskShape").setValueAtTime(inPoint + .3, myShape2);
    myMask.property("maskShape").setValueAtTime(outPoint - .3, myShape2);
    myMask.property("maskShape").setValueAtTime(outPoint, myShape1);
}
function animateCircle(comp, inPoint, outPoint, yPosition, color, size) {
    var opacity = 75;
    var animationDuration = .5;
    var layer = drawEllipse(comp, {
        "color": [37 * 1 / 255, 37 * 1 / 255, 37 * 1 / 255],
        "height": size,
        "opacity": opacity,
        "width": size
    });
    layer.position.setValue([
        layer.position.value[0],
        yPosition
    ]);
    layer.scale.setValueAtTime(inPoint, [0, 0]);
    layer.scale.setValueAtTime(inPoint + animationDuration, [100, 100]);
    layer.scale.setValueAtTime(outPoint - animationDuration, [100, 100]);
    layer.scale.setValueAtTime(outPoint, [0, 0]);
    layer.opacity.setValueAtTime(inPoint, 0);
    layer.opacity.setValueAtTime(inPoint + animationDuration, 100);
    layer.opacity.setValueAtTime(outPoint - animationDuration, 100);
    layer.opacity.setValueAtTime(outPoint, 0);
    // setScaleEase(layer, 1);
    // setScaleEase(layer, 2);
    // setScaleEase(layer, 3);
    // setScaleEase(layer, 4);
    layer.inPoint = inPoint;
    layer.outPoint = outPoint;
    return layer;
}
function drawEllipse(comp, options) {
    /*
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
function drawRoundedRectangle(comp, options) {
    var layer = drawSquare(comp, {
        "color": options.color || [1, 1, 1],
        "height": options.size,
        "opacity": options.opacity || 100,
        "radius": options.size >> 1,
        "width": options.size
    });
    var shapeGroup = layer.property("Contents").property("ADBE Vector Group");
    var shapeGroupContents = shapeGroup.property("Contents");
    var myRect = shapeGroupContents.property("ADBE Vector Shape - Rect");
    var rectSize = myRect.property("ADBE Vector Rect Size");
    //var rectPosition = myRect.property("ADBE Vector Rect Position");
    rectSize.setValueAtTime(options.inPoint, [options.size, options.size]);
    rectSize.setValueAtTime(options.inPoint + options.duration, [options.size + options.offset, options.size]);
    // rectPosition.setValueAtTime(options.inPoint, [0, 0]);
    // rectPosition.setValueAtTime(options.inPoint + options.duration, [-options.offset / 2, 0]);
    rectSize.setValueAtTime(options.outPoint - options.duration, [options.size + options.offset, options.size]);
    rectSize.setValueAtTime(options.outPoint, [options.size, options.size]);
    //rectPosition.setValueAtTime(options.outPoint - options.duration, [-options.offset / 2, 0]);
    //rectPosition.setValueAtTime(options.outPoint, [0, 0]);
    setEase(rectSize, 1);
    setEase(rectSize, 2);
    setEase(rectSize, 3);
    setEase(rectSize, 4);
    layer.inPoint = options.inPoint;
    layer.outPoint = options.outPoint;
    return layer;
    // app.project.activeItem.width * (1 - .618)
    //dumpPropTree(app.project.activeItem.selectedLayers[0].selectedProperties[0], 0);
}
function drawSquare(comp, options) {
    /*
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
    shapeGroupContents.addProperty("ADBE Vector Filter - RC")
        .property('ADBE Vector RoundCorner Radius').setValue(options.radius);
    return layer;
}
function drawText(comp, options) {
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
function dumpKeyframeEase(property, i) {
    var array = [];
    try {
        var keyInInterpolationType = property.keyInInterpolationType(i).toString();
        array.push("\nkeyInInterpolationType: " + keyInInterpolationType);
    } catch (error) { }
    try {
        var keyInSpatialTangent = property.keyInSpatialTangent(i).toString();
        array.push("\nkeyInSpatialTangent: " + keyInSpatialTangent);
    } catch (error) { }
    try {
        var keyInTemporalEase = property.keyInTemporalEase(i).toString();
        array.push("\nkeyInTemporalEase: " + keyInTemporalEase);
        for (var index = 0; index < property.keyInTemporalEase(i).length; index++) {
            var element = property.keyInTemporalEase(i)[index];
            $.writeln(index + '. speed: ' + element.speed + '; influence: ' + element.influence)
        }
    } catch (error) { }
    try {
        var keyLabel = property.keyLabel(i).toString();
        array.push("\nkeyLabel: " + keyLabel);
    } catch (error) { }
    try {
        var keyOutInterpolationType = property.keyOutInterpolationType(i).toString();
        array.push("\nkeyOutInterpolationType: " + keyOutInterpolationType);
    } catch (error) { }
    try {
        var keyOutSpatialTangent = property.keyOutSpatialTangent(i).toString();
        array.push("\nkeyOutSpatialTangent: " + keyOutSpatialTangent);
    } catch (error) { }
    try {
        var keyOutTemporalEase = property.keyOutTemporalEase(i).toString();
        array.push("\nkeyOutTemporalEase: " + keyOutTemporalEase);
        for (var index = 0; index < property.keyOutTemporalEase(i).length; index++) {
            var element = property.keyOutTemporalEase(i)[index];
            $.writeln(index + '. speed: ' + element.speed + '; influence: ' + element.influence)
        }
    } catch (error) { }
    try {
        var keyRoving = property.keyRoving(i).toString();
        array.push("\nkeyRoving: " + keyRoving);
    } catch (error) { }
    try {
        var keySelected = property.keySelected(i).toString();
        array.push("\nkeySelected: " + keySelected);
    } catch (error) { }
    try {
        var keySpatialAutoBezier = property.keySpatialAutoBezier(i).toString();
        array.push("\nkeySpatialAutoBezier: " + keySpatialAutoBezier);
    } catch (error) { }
    try {
        var keySpatialContinuous = property.keySpatialContinuous(i).toString();
        array.push("\nkeySpatialContinuous: " + keySpatialContinuous);
    } catch (error) { }
    try {
        var keyTemporalAutoBezier = property.keyTemporalAutoBezier(i).toString();
        array.push("\nkeyTemporalAutoBezier: " + keyTemporalAutoBezier);
    } catch (error) { }
    try {
        var keyTemporalContinuous = property.keyTemporalContinuous(i).toString();
        array.push("\nkeyTemporalContinuous: " + keyTemporalContinuous);
    } catch (error) { }
    try {
        var keyTime = property.keyTime(i).toString();
        array.push("\nkeyTime: " + keyTime);
    } catch (error) { }
    try {
        var keyValue = property.keyValue(i).toString();
        array.push("\nkeyValue: " + keyValue);
    } catch (error) { }
    $.writeln(array.join(''));
    /*
    dumpKeyframeEase(
    app.project.activeItem.selectedProperties[0].property("ADBE Vector Rect Size"),
    1
)
    */
}
function setEase(property, index) {
    var vi = new KeyframeEase(0, 75);
    property.setTemporalEaseAtKey(index, [
        vi, vi
    ], [vi, vi]);
    property.setInterpolationTypeAtKey(index, 6613)
}
function setScaleEase(layer, index) {
    var vi = new KeyframeEase(0, 75);
    var vo = new KeyframeEase(0, 33.333333);
    layer.scale.setTemporalEaseAtKey(index, [
        vi, vi, vo
    ], [vi, vi, vo]);
    layer.scale.setInterpolationTypeAtKey(index, 6613)
}
function setPositionEase(y, index) {
    var vi = new KeyframeEase(0, 75);
    y.setTemporalEaseAtKey(index, [
        vi
    ], [vi]);
    y.setInterpolationTypeAtKey(index, 6613)
}
var comp = app.project.activeItem;
function drawLine(comp, options) {


    var text = options.text || "";
    var inPoint = options.inPoint || 0;
    var outPoint = options.outPoint || 0;
    var fontSize = options.fontSize || 52;
    var rectHeight = options.rectHeight || 100;
    var y = options.y || 660;
    var x = options.x || 0;
    var textYOffset = options.textYOffset || 19;

    var textLayer = drawText(comp, {
        "color": [0 / 1 / 255, 0 / 1 / 255, 0 / 1 / 255],
        "font": "",
        "fontSize": fontSize,
        "inPoint": inPoint,
        "justification": ParagraphJustification.CENTER_JUSTIFY,
        "outPoint": outPoint,
        "text": text
    });
    textLayer.position.setValue(
        [x, y + textYOffset]
    )
    var textLayerRect = textLayer.sourceRectAtTime(inPoint, false);
    var layer = drawSquare(comp, {
        "color": [1, 1, 1],
        "height": rectHeight,
        "opacity": 100,
        "radius": rectHeight >> 1,
        "width": textLayerRect.width + rectHeight
    });
    x = (textLayerRect.width + rectHeight) / 2 + x;
    layer.position.setValue([
        x,
        y
    ])
    layer.moveAfter(textLayer);
    layer.opacity.setValueAtTime(inPoint, 0);
    layer.opacity.setValueAtTime(inPoint + 0.5, 100);
    layer.opacity.setValueAtTime(outPoint - 0.5, 100);
    layer.opacity.setValueAtTime(outPoint, 0);
    textLayer.position.setValueAtTime(inPoint, [x - (rectHeight >> 1), y + textYOffset]);
    textLayer.position.setValueAtTime(inPoint + .5, [x, y + textYOffset]);
    textLayer.opacity.setValueAtTime(inPoint, 0);
    textLayer.opacity.setValueAtTime(inPoint + .5, 100);
    textLayer.position.setValueAtTime(outPoint - .5, [x, y + textYOffset]);
    textLayer.position.setValueAtTime(outPoint, [x - (rectHeight >> 1), y + textYOffset]);
    textLayer.opacity.setValueAtTime(outPoint - 0.5, 100);
    textLayer.opacity.setValueAtTime(outPoint, 0);
    setPositionEase(textLayer.position, 1);
    setPositionEase(textLayer.position, 2);
    setPositionEase(textLayer.position, 3);
    setPositionEase(textLayer.position, 4);
}



function drawLine1280x720(text, inPoint, outPoint) {
    var fontSize = 52 * .75;
    var rectHeight = 100 * .75;
    var x = 72;
    var y = 560;
    var textYOffset = 19 * .75;

    drawLine(comp, {
        text: text,
        inPoint: inPoint,
        outPoint: outPoint,
        x: x,
        y: y,
        fontSize: fontSize,
        rectHeight: rectHeight,
        textYOffset: textYOffset
    });
}
function parseSeconds(time, fps) {
    var matched = /(\d{1}):(\d{2}):(\d{2}):(\d{2})/.exec(time);
    var seconds = parseInt(matched[1]) * 3600
        + parseInt(matched[2]) * 60
        + parseInt(matched[3])
        + parseInt(matched[4]) * 1 / fps
    return seconds;
}
drawLine1280x720('Ctrl+Enter键填充所选单元格', 
parseSeconds('0:01:01:16',30),
parseSeconds('0:01:13:02',30)
);
