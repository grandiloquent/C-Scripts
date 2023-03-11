(()=>{
    var times=`0:00:23:00
    0:00:26:10
    0:00:31:05
    0:00:47:23
    0:01:14:00`
const array=times.split('\n');
let seconds=0;
var buf=[];
for (let index = 0; index < array.length; index++) {
    const element = array[index];
    var matched=/\d:+\d{2}:(\d{2}):(\d{2})/.exec(element);
    var last=parseInt(matched[1])+parseInt(matched[2])*1/30;;
    if(seconds!=0){
var str=`ffmpeg -ss ${seconds} -t ${last-seconds} -i 0.mp4 ${index}.mp4`;
buf.push(str);
    }
    seconds= last;
}
console.log(buf.join('\n'))
})();