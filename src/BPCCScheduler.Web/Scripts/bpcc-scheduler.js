/*** DMK 2013-02-10 ***/

var myApp = angular.module('myApp', ['ui']);

var scheduleController = function ($scope, $http) {

    ////*******    Reference Data for drop downs *********/////
    $scope.times = [{
        "value": "10:15",
        "label": "10:15 am",
        "hour": 10,
        "minute": 15
    }, {
        "value": "11:00",
        "label": "11:00 am",
        "hour": 11,
        "minute": 0
    }, {
        "value": "17:15",
        "label": "5:15 pm",
        "hour": 17,
        "minute": 15
    }, {
        "value": "17:45",
        "label": "5:45 pm",
        "hour": 17,
        "minute": 45
    }, {
        "value": "18:00",
        "label": "6:00 pm",
        "hour": 18,
        "minute": 0
    }, {
        "value": "18:30",
        "label": "6:30 pm",
        "hour": 18,
        "minute": 30
    }];

    $scope.weekDays = [{
        "label": "Monday",
        "value": 1
    }, {
        "label": "Tuesday",
        "value": 2
    }, {
        "label": "Wednesday",
        "value": 3
    }, {
        "label": "Thursday",
        "value": 4
    }, {
        "label": "Friday",
        "value": 5
    }];

    /////*******   View data   *****/////

    $scope.title = "Schedule";

    $scope.message = {
        "text": "Howdy!",
        "cssClass": "alert alert-info hidden"
    };


    //////******   MODEL Data   ************/////

    //$scope.schedule = [{
    //    "ClientName": "Posh",
    //    "Cell": "7165551212",
    //    "Date": "2013-03-01T22:15:00Z"
    //}, {
    //    "ClientName": "Baby",
    //    "Cell": "7165553333",
    //    "Date": "2013-02-25T15:15:00Z"
    //}, {
    //    "ClientName": "Scary",
    //    "Cell": "7165556666",
    //    "Date": "2013-02-26T22:45:00Z"
    //}, {
    //    "ClientName": "Sporty",
    //    "Cell": "7165555555",
    //    "Date": "2013-02-27T23:00:00Z"
    //}, {
    //    "ClientName": "Ginger",
    //    "Cell": "7705550770",
    //    "Date": "2013-02-28T23:30:00Z"
    //}];


    $scope.sort = "Date";


    ///******  actions    *****///

    $scope.addAppt = function (appt) {

        //guard - already in list
        if ($scope.schedule.indexOf(appt) > -1) {
            $scope.clearSelectedAppointment();
            return;
        }

        $scope.schedule.push(appt);
        //$scope.clearSelectedAppointment();
    };

    $scope.getClass = function (appt) {
        if (appt === $scope.selAppt) {
            return "row-selected";
        }
    };

    $scope.deleteAppt = function (appt) {
        //guard
        if (!confirm("Are you sure you want to delete this appointment?")) {
            return;
        }

        var index = $scope.schedule.indexOf(appt);
        $scope.schedule.splice(index, 1);
    };

    $scope.editAppt = function (appt) {
        var index = $scope.schedule.indexOf(appt);
        //$scope.selAppt = angular.copy($scope.schedule[i]);
        $scope.selAppt = $scope.schedule[index];
        $scope.weekDay = $scope.getWeekDay($scope.selAppt.Date).value;
        var aDate = new Date($scope.selAppt.Date);
        $scope.time = aDate.getHours().toString() + ":" + aDate.getMinutesTwoDigits().toString();
    };

    $scope.getWeekDay = function (dateVal) {
        var date = new Date(dateVal);
        var weekday = $scope.weekDays[date.getDay() - 1];
        return weekday;
    };

    $scope.getWeekDayLabel = function (dateVal) {
        return $scope.getWeekDay(dateVal).label;
    };

    $scope.dayChanged = function (appt) {
        appt.Date = dateOfNext($scope.weekDay).toISOString();
    };

    $scope.timeChanged = function (appt) {
        //get selected time object from Times array
        var time = findByValue($scope.times, $scope.time);
        var newDate = new Date(appt.Date);
        newDate.setHours(time.hour);
        newDate.setMinutes(time.minute);
        appt.Date = newDate.toISOString();
    };

    $scope.addWeek = function (appt) {
        var aDate = new Date(appt.Date);
        var nextWeek = addDays(aDate, 7);
        appt.Date = nextWeek.toISOString();
    };

    $scope.subtractWeek = function (appt) {
        var aDate = new Date(appt.Date);
        var prevWeek = subtractDays(aDate, 7);
        //guard, don't let dates in the past get assigned
        if (prevWeek < subtractDays(new Date(), 1)) {
            return;
        }
        appt.Date = prevWeek.toISOString();
    };

    $scope.clearSelectedAppointment = function () {

        $scope.selAppt = {
            "ClientName": "",
            "Cell": "716",
            "Date": new Date().toISOString()
        };
    };


    $scope.postSchedule = function () {
        $http.put(
                '/api/ScheduleAll',
                JSON.stringify($scope.schedule)
            ).success(
                alert("Schedule Saved")
        );
    };


    $scope.init = function () {
        //clear the selected appt
        $scope.clearSelectedAppointment();
        $http.get('api/ScheduleAll').success(function (data) {
            $scope.schedule = data;
        });
    };


    ////****** INIT   ******///
    $scope.init();




}; //end of Schedule Controller



////****** general javascript functions (no dependencies)   *****//////

function addDays(dateVal, days) {
    return new Date(dateVal.getTime() + days * 24 * 60 * 60 * 1000);
}

function subtractDays(dateVal, days) {
    return new Date(dateVal.getTime() - days * 24 * 60 * 60 * 1000);
}

function dateOfNext(weekdayNumber) {
    var today = new Date();
    var lastSunday = subtractDays(today, today.getDay());
    var daysToAdd = weekdayNumber;
    if (weekdayNumber <= today.getDay()) {
        daysToAdd = daysToAdd + 7;
    }
    var nextDate = addDays(lastSunday, daysToAdd);
    //alert("weekdayNumber:" + weekdayNumber + ", today:" + today + ", lastSunday:" + lastSunday + ", daysToAdd:" + daysToAdd + ", nextDate:" + nextDate);
    return nextDate;
}

function findByValue(array, value) {
    for (var i = 0, len = array.length; i < len; i++) {
        if (array[i].value === value) {
            return array[i];
        }
    }
    return null; //nothing found
}

Date.prototype.getMinutesTwoDigits = function () {
    var retval = this.getMinutes();
    if (retval < 10) {
        return ("0" + retval.toString());
    } else {
        return retval.toString();
    }
};

//////***** Angular Filters **********////

angular.module('ng').filter('tel', function () {
    return function (tel) {
        if (!tel) { return ''; }

        var value = tel.toString().trim().replace(/^\+/, '');

        if (value.match(/[^0-9]/)) {
            return tel;
        }

        var country, city, number;

        switch (value.length) {
            case 10: // +1PPP####### -> C (PPP) ###-####
                country = 1;
                city = value.slice(0, 3);
                number = value.slice(3);
                break;

            case 11: // +CPPP####### -> CCC (PP) ###-####
                country = value[0];
                city = value.slice(1, 4);
                number = value.slice(4);
                break;

            case 12: // +CCCPP####### -> CCC (PP) ###-####
                country = value.slice(0, 3);
                city = value.slice(3, 5);
                number = value.slice(5);
                break;

            default:
                return tel;
        }

        if (country == 1) {
            country = "";
        }

        number = number.slice(0, 3) + '-' + number.slice(3);

        return (country + " (" + city + ") " + number).trim();
    };
});


