// Documentation for Libs (installed with Nuget and others)
// - http://nodatime.org/

open System
open System.Globalization
open System.Xml

open NodaTime

open HolyDayCalendar


[<EntryPoint>]
let main argv = 
    let nowNative = DateTime.Now

    // numbering systems
    let cu = Calendars.HebrewMonthNumbering.Civil
    let su = Calendars.HebrewMonthNumbering.Scriptural

    // setup calendar systems
    let cuCal = CalendarSystem.GetHebrewCalendar cu
    let suCal = CalendarSystem.GetHebrewCalendar su
    let myCal = CalendarSystem.GetJulianCalendar 2

    // "my" calendar dates
    let nowNoda = new LocalDate(nowNative.Year, nowNative.Month, nowNative.Day)
    let myTrump = new LocalDate(2015, 9, 14)

    // hebrew calendar dates
    let trumpets = new LocalDate(5775, 7, 1, suCal)
    let atone = new LocalDate(5775, 7, 10, suCal)

    // conversions
    let myTrumpHeb = myTrump.WithCalendar suCal // confirmed
    let nowNodaHeb = nowNoda.WithCalendar suCal
    let myTrumpets = trumpets.WithCalendar myCal // fail by 2 days early (Sep. 12)
    let myAtone = atone.WithCalendar myCal // fail by 2 days (Sep. 21)

    // TODO pass-thru TimeSpan, find nearest
    let findTrumpets (d: LocalDate) =
        let hd = d.WithCalendar suCal
        let rec g (d: LocalDate) =
            if d.Day > 1 then
                g (d.PlusDays -1)
            else
                d

        let rec f (d: LocalDate) =
            if d.Month < 7 then
                f (d.PlusMonths 1)
            else if d.Month > 7 then
                f (d.PlusMonths -1)
            else
                g d
        let tr: LocalDate = f hd
        tr.WithCalendar myCal

    let found = findTrumpets nowNoda // 12 Sep !!!!!



    printfn "HebCivil: %A" nowNodaHeb
    printfn "Trumpets: %A" (trumpets.WithCalendar(myCal))
    printfn "Press Enter: "
    let input = Console.ReadLine()
    0 // return an integer exit code