module HolyDays


type DayOfWeek  =
    | UNSET     = 0
    | Sunday    = 1
    | Monday    = 2
    | Tuesday   = 3
    | Wednesday = 4
    | Thursday  = 5
    | Friday    = 6
    | Saturday  = 7

type SolarMonth =
    | January   = 0
    | February  = 1
    | March     = 2
    | April     = 3
    | May       = 4
    | June      = 5
    | July      = 6
    | August    = 7
    | September = 8
    | October   = 9
    | November  = 10
    | December  = 11

type LunarMonth   =
    | Tishri      = 0
    | Heshvan     = 1
    | Kislev      = 2
    | Tebeth      = 3
    | Shebat      = 4
    | ``Adar I``  = 5
    | ``Adar II`` = 6
    | Nisan       = 7
    | Iyar        = 8
    | Sivan       = 9
    | Tammuz      = 10
    | Ab          = 11
    | Elul        = 12
    | Adar        = 13

type NamedMonth = | SolarMonth | LunarMonth

type Month(num: int, name: NamedMonth, numDays: int) =
    let MonthNumber = num
    let MonthName   = name
    let NumDays     = numDays

type Year(num, isLeap) =
    let yearNum    = num
    let isLeapYear = isLeap

    new(num) =
        new Year(num, false)


module HolyDay =

    type HolyDay =
        {
            Name:    string;
            Month:   int option;
            Day:     int option;
            Weekday: DayOfWeek option;
            Length:  int;
        }

    let Days =
        // rules found from
        // http://afaithfulversion.org/leviticus-23/
        [
            // 14th day of the 1st month
            {
                Name    = "Passover";
                Month   = Some 1;
                Day     = Some 14;
                Weekday = None;
                Length  = 1;
            };
            { // Additional feast from Numbers 9:6-13
                Name    = "2nd Passover";
                Month   = Some 2;
                Day     = Some 14;
                Weekday = None;
                Length  = 1;
            };
            // succeeding day after passover
            {
                Name    = "Feast of Unleavened Bread";
                Month   = Some 1;
                Day     = Some 15;
                Weekday = None;
                Length  =  7;
            };
            // 49th day after Sabbath following Unleavened Bread
            {
                Name    = "Pentecost";
                Month   = None;
                Day     = Some 50;
                Weekday = Some DayOfWeek.Saturday;
                Length  = 1;
            };
            // 1st day of 7th month
            {
                Name    = "Feast of Trumpets";
                Month   = Some 7;
                Day     = Some 1;
                Weekday = None;
                Length  = 1;
            };
            // 10th day of 7th month is Atonement
            {
                Name    = "Day of Atonement";
                Month   = Some 7;
                Day     = Some 10;
                Weekday = None;
                Length  = 1;
            };
            // 15th day of 7th month starts the week-long feast
            {
                Name    = "Feast of Tabernacles";
                Month   = Some 7;
                Day     = Some 15;
                Weekday = None;
                Length  = 7;
            };
            {
                Name    = "Last Great Day";
                Month   = Some 7;
                Day     = Some 22;
                Weekday = Some DayOfWeek.Saturday;
                Length  = 1;
            };
        ]

open NodaTime

let getDaysFromSaturday day =
    let daysPrev = 0
    let rec f (day: LocalDate) backcount =
        match day.DayOfWeek with
        | 6 -> backcount
        | _ ->
            let span = Period.FromDays(int64 1)
            let prevDay = day - span
            f prevDay (backcount + 1)
    f day daysPrev

let findFirstSaturday (thisDay: LocalDate) =
    let thisNewYears = new LocalDate(thisDay.Year, 1, 1)
    let rec f (day: LocalDate) =
        match day.DayOfWeek with
        | 6 -> day
        | _ ->
            let nextDay = day + Period.FromDays(int64 1)
            f nextDay
    f thisNewYears
