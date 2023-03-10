(()=>{
    var times=`0:00:03:00
0:00:04:20
0:00:06:18
0:00:12:11
0:00:19:14
0:00:23:02`
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