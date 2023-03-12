function anchorLeft(square, width) {
    // 锚点默认位于对象中心，所以如果左移宽度的一半，则可以将锚点移到左端
    square.anchorPoint.setValue([
        square.anchorPoint.value[0]
        -width / 2,square.anchorPoint.value[1], 0]);
  var position = square.position.value;
    // 因为锚点左移，位置的X轴也应等量左移，以保持对象原来的位置
   square.position.setValue([position[0] - width / 2, position[1], position[2]])
}
function setPositionEase(layer, index) {
    var vi = new KeyframeEase(0, 75);
    layer.position.setTemporalEaseAtKey(index, [
        vi
    ], [vi]);
    layer.position.setInterpolationTypeAtKey(index, 6613)
}
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
var comp=app.project.activeItem;

var selectedLayer=comp.selectedLayers[0];
var selectedLayerRect=selectedLayer.sourceRectAtTime(0,false);

//comp.addGuide(1,90)

var xOffset=0;
var inPoint=parseSeconds("0:00:09:24",20);
var outPoint=inPoint+3;

selectedLayer.position.setValue([
    selectedLayer.width/2+xOffset,
    560
])
anchorLeft(selectedLayer,selectedLayer.width)

// selectedLayer.scale.setValueAtTime(0,[0,100]);
// selectedLayer.scale.setValueAtTime(.35,[100,100]);
selectedLayer.position.setValueAtTime(inPoint,[
    selectedLayer.width*-1,
560
]);
selectedLayer.position.setValueAtTime(inPoint+0.35,[
   0,
560
]);

selectedLayer.position.setValueAtTime(outPoint-0.35,[
    0,
 560
 ]);
selectedLayer.position.setValueAtTime(outPoint,[
    selectedLayer.width*-1,
560
]);


setPositionEase(selectedLayer, 1);
setPositionEase(selectedLayer, 2);
setPositionEase(selectedLayer, 3);
setPositionEase(selectedLayer, 4);

selectedLayer.inPoint=inPoint;
selectedLayer.outPoint=outPoint;