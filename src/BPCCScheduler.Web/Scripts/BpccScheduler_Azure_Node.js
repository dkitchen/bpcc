function bpccReminders() {

    /*jshint multistr: true */  //allows backslash concat

    var logMsg = "The BPCC Reminders Job is Running";

    var now = new Date().getEasternTime();

    var nowString = now.toJSON();
    logMsg = logMsg + " at " + nowString;

    //need to run each query ONLY at 7pm, 9am, and 3pm
    var nowHour = now.getHours();

    logMsg = logMsg + ", nowHour = " + nowHour;

    var query = "";
    var endDate;
    switch (nowHour) {
        case 19:    //7pm
            //get tomorrow's appointments
            query = "Tomorrow"
            break;
        case 9:     //9am            
            endDate = now.addHours(3);  //noon today
            query = "TodayAM";
            break;
        case 15:    //3pm
            endDate = now.addHours(5);  //8pm tonight
            query = "TodayPM";
            break;
        default:
            break;
    }

    //debug
    query = "Tomorrow";

    if (query.length > 0) {
        logMsg = logMsg + ", should generate SMSs: " + query;
        var request = require("request");
        var uri = "http://bpcc.azurewebsites.net/api/sms" + query;
        logMsg = logMsg + " request: " + uri;

        request(uri,
            function (err, response, body) {
                if (err) {
                    console.error(err);
                }
                //console.info(response);
                console.info(body);
            }
        );
    }
    else {
        logMsg = logMsg + ", No SMSs should be sent now.";
    }

    console.info(logMsg);

}

Date.prototype.addSeconds = function (seconds) {
    return new Date(this.getTime() + seconds * 1000);   //add milliseconds
}

Date.prototype.addMinutes = function (minutes) {
    return this.addSeconds(minutes * 60);
}

Date.prototype.addHours = function (hours) {
    return this.addMinutes(hours * 60);
}

Date.prototype.getEasternTime = function () {
    return this.addHours(-4);  //-5 in the fall
}