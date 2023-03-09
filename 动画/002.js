var comp = app.project.activeItem;

var layers = [];
for (var i = 0; i < comp.numLayers; i++) {
    if (/^\d+\.mp3$/.test(comp.layer(i + 1).name)) {
        var layer = comp.layer(i + 1);
        layers.push(layer);

        
    }
}
layers=layers.sort(function(x,y){
    return parseInt(x.name)-parseInt(y.name)
})
var start=0;
for(var i=0;i<layers.length;i++){
    layers[i].startTime=start;
    var layer=findLayer((i+1).toString()+".mp4");
    layer.startTime=start;
    start+=layers[i].outPoint - layers[i].inPoint
   
    // $.writeln(findLayer((i+1).toString()).outPoint);
    
}

function findLayer(name) {
    for (var i = 0; i < comp.numLayers; i++) {
        if (name === comp.layer(i + 1).name) {
            return comp.layer(i + 1);
        }
    }
    return null;
}