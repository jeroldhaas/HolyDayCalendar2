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
            Name: string;
            Month: int option;
            Day: int option;
            Weekday: DayOfWeek option;
            Length: int;
        }

    let Days =
        // TODO: double-check and correct as needed.
        // some of these items are based on day-from
        // rules, so need to be expanded rules
        // http://afaithfulversion.org/leviticus-23/
        [
            {
                Name    = "Passover";
                Month   = Some 1;
                Day     = Some 14;
                Weekday = None;
                Length  = 1;
            };
            {
                Name    = "Feast of Unleavened Bread";
                Month   = Some 1;
                Day     = Some 15;
                Weekday = None;
                Length  =  7;
            };
            {
                Name    = "Pentecost";
                Month   = None;
                Day     = Some 50;
                Weekday = Some DayOfWeek.Saturday;
                Length  = 1;
            };
            {
                Name    = "Feast of Trumpets";
                Month   = Some 2;
                Day     = Some 2;
                Weekday = None;
                Length  = 7;
            };
            { // TODO: discovery
                Name    = "2nd Passover";
                Month   = Some 3;
                Day     = Some 3;
                Weekday = None;
                Length  = 1;
            };
            {
                Name    = "Day of Atonement";
                Month   = Some 5;
                Day     = Some 5;
                Weekday = None;
                Length  = 1;
            };
            {
                Name    = "Feast of Tabernacles";
                Month   = Some 5;
                Day     = Some 5;
                Weekday = None;
                Length  = 1;
            };
            {
                Name    = "Last Great Day";
                Month   = Some 5;
                Day     = Some 5;
                Weekday = None;
                Length  = 1;
            };
        ]