var comp = app.project.activeItem;
var layer = comp.selectedLayers[0];
var width = comp.width;
var inPoint =47+15*1/30;

layer.scale.setValueAtTime(inPoint, [100, 100]);
layer.scale.setValueAtTime(inPoint + .5, [200, 200]);
layer.scale.setValueAtTime(inPoint + .35 + 2, [200, 200]);
layer.scale.setValueAtTime(inPoint + .35 + 2 + .5, [100, 100]);

//comp.addGuide(1,640);
var x = 340;
var y =44;
comp.addGuide(2, 360);
var times=[
    inPoint,
    inPoint + .5,inPoint + .35 + 2,inPoint + .35 + 2 + .5,
]
layer.position.setValueAtTime(times[0], [640, 360]);

var halfX=comp.width/2;
var halfY= comp.height / 2;
var transformedX =  halfX- halfX * 1 - (x - (comp.width - x) * 1 - halfX);
var transformedY =  halfY - halfY* 1 - (y - (comp.height - y) * 1 - halfY);

layer.position.setValueAtTime(times[1], [
    transformedX,
    transformedY
])
layer.position.setValueAtTime(times[2], [
    transformedX,
    transformedY
])
layer.position.setValueAtTime(times[3], [640, 360]);
/*
var comp=app.project.activeItem;
var layer=comp.layer(1);
var width=comp.width;
$.writeln(width*.3);
layer.scale.setValue([130,130]);
layer.position.setValue([
    layer.position.value[0]-(width*.3)/2,
    layer.position.value[1]
])

*/
/*
var comp=app.project.activeItem;
var layer=comp.selectedLayers[0];
var width=comp.width;
var inPoint=16+20*1/30;

layer.scale.setValueAtTime(inPoint,[100,100]);
layer.scale.setValueAtTime(inPoint+.5,[150,150]);
layer.scale.setValueAtTime(inPoint+.35+5,[150,150]);
layer.scale.setValueAtTime(inPoint+.35+5+.5,[100,100]);

//comp.addGuide(1,640);
var x=1220;
var y=705;
comp.addGuide(2,360);
layer.position.setValueAtTime(inPoint,[640,360]);
layer.position.setValueAtTime(inPoint+.5,[
    640-comp.width/2*.5-(x-(x-comp.width)*.5-comp.width/2),
   360-comp.height/2*.5-(y-(y-comp.height)*.5-comp.height/2)
])
layer.position.setValueAtTime(inPoint+.35+5,[
    640-comp.width/2*.5-(x-(x-comp.width)*.5-comp.width/2),
   360-comp.height/2*.5-(y-(y-comp.height)*.5-comp.height/2)
])
layer.position.setValueAtTime(inPoint+.35+5+.5,[640,360]);
*/