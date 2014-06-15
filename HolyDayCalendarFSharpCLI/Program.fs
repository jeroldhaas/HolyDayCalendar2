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
    let nowGregorian = new LocalDateTime(
                        nowNative.Year,
                        nowNative.Month,
                        nowNative.Day,
                        nowNative.Hour,
                        nowNative.Minute,
                        CalendarSystem.GetGregorianCalendar(1)
    )
    let nowCoptic = new LocalDateTime(
                        nowNative.Year,
                        nowNative.Month,
                        nowNative.Day,
                        nowNative.Hour,
                        nowNative.Minute,
                        CalendarSystem.GetCopticCalendar(1)
    ) // TODO: adjust int if needed
    
    // duh - print the results :)
    printfn "Native:\t\t %A" (nowNative.Date.ToLongDateString())
    printfn "Gregorian:\t %A" (nowGregorian.Date.ToString())
    printfn "Coptic:\t\t %A" (nowCoptic.Date.ToString()) 
    printfn "Press Enter: "
    let input = Console.ReadLine()
    0 // return an integer exit code