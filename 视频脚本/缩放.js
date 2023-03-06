var comp=app.project.activeItem;
var layer=comp.layer(1);
var width=comp.width;
$.writeln(width*.3);
layer.scale.setValue([130,130]);
layer.position.setValue([
    layer.position.value[0]-(width*.3)/2,
    layer.position.value[1]
])
