module HolyDayTests

open System
open System.Xml

open FsUnit
open FsCheck
open NodaTime

//let ``check conversion from one calendar to the other`` =
let now = System.DateTime.Now
let nodaNow = new NodaTime.LocalDate(now.Year, now.Month, now.Day)

let cn = Calendars.HebrewMonthNumbering.Civil
let bn = Calendars.HebrewMonthNumbering.Scriptural

let cnHebCalsys = CalendarSystem.GetHebrewCalendar cn
let bnHebCalsys = CalendarSystem.GetHebrewCalendar bn


let cnHebNodaNow = nodaNow.WithCalendar cnHebCalsys
let bnHebNodaNow = nodaNow.WithCalendar bnHebCalsys
