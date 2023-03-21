
function setEnd(time) {
    var seq = app.project.activeSequence;
    var clips = seq.videoTracks[0].clips;
    for (var index = 0; index < clips.length; index++) {
        var clip = clips[index];
        if (clip.isSelected()) {
            var end = clip.end;
            end.seconds = parseSeconds(time,30);
            clip.end = end;
        }
    }
} 
function parseSeconds(time, fps) {
    var matched = /(\d{2}):(\d{2}):(\d{2}):(\d{2})/.exec(time);
    var seconds = parseInt(matched[1]) * 3600
        + parseInt(matched[2]) * 60
        + parseInt(matched[3])
        + parseInt(matched[4]) * 1 / fps
    return seconds;
}
setEnd('00:01:36:04')