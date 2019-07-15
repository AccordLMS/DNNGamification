Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Services.Exceptions

Namespace Interzoic.LMS.CommonControls

    Partial Public Class DateRangeSelector
        Inherits IzmLmsUserControlBase

#Region "Private Members"

        Private _moduleId As Integer = Null.NullInteger
        Private _DateRange As String = Null.NullString
        Private _Interval As Interval = Interval.None
        Private _StartDate As Date = Null.NullDate
        Private _EndDate As Date = Null.NullDate

        Private _ShowInterval As Boolean = False
        Private _ShowCustom As Boolean = True

#End Region

#Region "public const"
        Enum Interval
            Hour = 0
            Year = 1
            Month = 2
            Day = 3
            None = 4
        End Enum

#End Region

#Region "public properties"

        ''' <summary>
        ''' present or not the text labels
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public WriteOnly Property ShowLabels() As Boolean
            Set(ByVal value As Boolean)
                tdStartDate.Visible = value
                tdEndDate.Visible = value
                tdRBLInterval.Visible = value
            End Set
        End Property

        ''' <summary>
        ''' presents or not the option to choose a custom start and end date by
        ''' entering each date
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShowCustom() As Boolean
            Get
                Return _ShowCustom
            End Get
            Set(ByVal value As Boolean)
                _ShowCustom = value
                ViewState("ShowCustom") = value
            End Set
        End Property

        ''' <summary>
        ''' Valid values:
        ''' Consts.DateRangeAllTime = "AllTime"
        ''' Consts.DateRangeToday = "Today"
        ''' Consts.DateRangeYesterday = "Yesterday"
        ''' Consts.DateRangeThisWeek = "ThisWeek"
        ''' Consts.DateRangeLast7Days = "Last7Days"
        ''' Consts.DateRangeLastWeek = "LastWeek"
        ''' Consts.DateRangeThisMonth = "ThisMonth"
        ''' Consts.DateRangeLast30Days = "Last30Days"
        ''' Consts.DateRangeLastMonth = "LastMonth"
        ''' Consts.DateRangeCustom = "Custom"
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DateRange() As String
            Get
                If Not cmbDateRange.SelectedItem Is Nothing Then
                    Return cmbDateRange.SelectedValue.ToString()
                Else
                    Return Null.NullString
                End If
            End Get
            Set(ByVal value As String)
                _DateRange = value

                If cmbDateRange.Items.Count = 0 Then
                    LoadDateRange()
                End If
                Dim lItem As ListItem
                lItem = cmbDateRange.Items.FindByValue(value)
                If Not lItem Is Nothing Then
                    cmbDateRange.ClearSelection()
                    lItem.Selected = True
                End If
                ViewState("DateRange") = value
            End Set
        End Property

        ''' <summary>
        ''' Get returns the selected start date.
        ''' Set only valid if you seted custom date in daterange
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property StartDate() As Date
            Get
                Me.LoadSettings()
                If DateRange <> Consts.DateRangeAllTime Then
                    Return _StartDate
                Else
                    Return _StartDate
                End If
            End Get
            Set(ByVal value As Date)
                _StartDate = value
                ViewState("StartDate") = value
                If _DateRange = Consts.DateRangeCustom Then
                    TrStartDate.Style.Item("display") = String.Empty
                    rdpStartDate.SelectedDate = _StartDate
                End If
            End Set
        End Property

        ''' <summary>
        ''' Get returns the selected start date.
        ''' Set only valid if you seted custom date in daterange
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EndDate() As Date
            Get
                Me.LoadSettings()
                If DateRange <> Consts.DateRangeAllTime Then
                    Return _EndDate
                Else
                    Return _EndDate
                End If
            End Get
            Set(ByVal value As Date)
                _EndDate = value
                ViewState("EndDate") = value
                If _DateRange = Consts.DateRangeCustom Then
                    TrEndDate.Style.Item("display") = String.Empty
                    rdpEndDate.SelectedDate = _EndDate
                End If
            End Set
        End Property


        ''' <summary>
        ''' Get and sets the actual selected interval. See ShowInterval
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SelectedInterval() As Interval
            Get
                If TrInterval.Visible Then
                    If Not Me.RBLInterval.SelectedItem Is Nothing Then
                        Return CType([Enum].Parse(GetType(Interval), Me.RBLInterval.SelectedValue), Interval)
                    Else
                        Return Interval.None
                    End If
                Else
                    Return Interval.None
                End If
            End Get
            Set(ByVal value As Interval)
                _Interval = value
                If RBLInterval.Items.Count > 0 Then
                    RBLInterval.SelectedValue = value.ToString()
                End If
            End Set
        End Property

        ''' <summary>
        ''' if true shows the controls to choose an interval related with the 
        ''' start and end date. This is used in the traffic report.
        ''' (do the report from this startdate to this enddate with this interval)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShowInterval() As Boolean
            Get
                Return _ShowInterval
            End Get
            Set(ByVal value As Boolean)
                _ShowInterval = value
                TrInterval.Visible = value
                ViewState("ShowInterval") = value
            End Set
        End Property

        Public ReadOnly Property LocalResourceFile() As String
            Get
                Return Me.ResolveUrl(Localization.LocalResourceDirectory & "/DateRangeSelector.ascx")
            End Get
        End Property

#End Region

#Region "Private Methods"

        Private Sub LocalizeLabels()
            lblStartDate.Text = Localization.GetString("plStartDate.Text", LocalResourceFile)
            lblEndDate.Text = Localization.GetString("plEndDate.Text", LocalResourceFile)
            lblInterval.Text = Localization.GetString("plInterval", LocalResourceFile)
            valDateRange.Text = Localization.GetString("valDateRange.Text", LocalResourceFile)
        End Sub

        Private Function GetLastSunday(ByVal _date As Date) As Date
            While _date.DayOfWeek = DayOfWeek.Sunday
                _date.AddDays(-1)
            End While
            Return _date
        End Function

        Private Function Get2LastSunday(ByVal _date As Date) As Date
            Dim firstSunday As Boolean = False
            While _date.DayOfWeek = DayOfWeek.Sunday And firstSunday = True
                If _date.DayOfWeek = DayOfWeek.Sunday Then
                    firstSunday = True
                End If
                _date.AddDays(-1)
            End While
            Return _date
        End Function

        Private Sub LoadSettings()
            Dim ltimeSpan As TimeSpan

            If DateRange = Consts.DateRangeCustom Then
                If rdpStartDate.SelectedDate IsNot Nothing Then
                    _StartDate = rdpStartDate.SelectedDate.Value
                    _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                Else
                    _StartDate = Null.NullDate
                End If

                If rdpEndDate.SelectedDate IsNot Nothing Then
                    _EndDate = rdpEndDate.SelectedDate.Value
                    _EndDate = New DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59)
                Else
                    _EndDate = Null.NullDate
                End If

            Else
                _DateRange = cmbDateRange.SelectedValue
                Select Case _DateRange
                    Case Consts.DateRangeToday
                        _StartDate = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
                        _EndDate = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
                    Case Consts.DateRangeYesterday
                        ltimeSpan = New TimeSpan(1, 0, 0, 0)
                        _StartDate = DateTime.Now.Subtract(ltimeSpan)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        _EndDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 23, 59, 59)
                    Case Consts.DateRangeThisWeek
                        ltimeSpan = New TimeSpan(DateTime.Now.DayOfWeek, 0, 0, 0)
                        _StartDate = DateTime.Now.Subtract(ltimeSpan)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        ltimeSpan = New TimeSpan(7 - DateTime.Now.DayOfWeek - 1, 0, 0, 0)
                        _EndDate = DateTime.Now.Add(ltimeSpan)
                        _EndDate = New DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59)
                    Case Consts.DateRangeLast7Days
                        ltimeSpan = New TimeSpan(7, 0, 0, 0)
                        _StartDate = DateTime.Now.Subtract(ltimeSpan)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        _EndDate = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
                    Case Consts.DateRangeLastWeek
                        ltimeSpan = New TimeSpan(DateTime.Now.DayOfWeek + 7, 0, 0, 0)
                        _StartDate = DateTime.Now.Subtract(ltimeSpan)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        ltimeSpan = New TimeSpan(DateTime.Now.DayOfWeek + 1, 0, 0, 0)
                        _EndDate = DateTime.Now.Subtract(ltimeSpan)
                        _EndDate = New DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59)
                    Case Consts.DateRangeThisMonth
                        _StartDate = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        _EndDate = _StartDate.AddDays(Date.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - 1)
                        _EndDate = New DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59)
                    Case Consts.DateRangeLast30Days
                        ltimeSpan = New TimeSpan(30, 0, 0, 0)
                        _StartDate = DateTime.Now.Subtract(ltimeSpan)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        _EndDate = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
                    Case Consts.DateRangeLastMonth
                        _StartDate = New DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1)
                        _StartDate = New DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0)
                        _EndDate = New DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month))
                        _EndDate = New DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59)
                End Select
            End If
        End Sub

        Private Sub LoadDateRange()
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeAllTime, LocalResourceFile), Consts.DateRangeAllTime))
            Me.cmbDateRange.Items.FindByValue(Consts.DateRangeAllTime).Selected = True
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeToday, LocalResourceFile), Consts.DateRangeToday))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeYesterday, LocalResourceFile), Consts.DateRangeYesterday))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeThisWeek, LocalResourceFile), Consts.DateRangeThisWeek))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeLast7Days, LocalResourceFile), Consts.DateRangeLast7Days))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeLastWeek, LocalResourceFile), Consts.DateRangeLastWeek))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeThisMonth, LocalResourceFile), Consts.DateRangeThisMonth))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeLast30Days, LocalResourceFile), Consts.DateRangeLast30Days))
            Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeLastMonth, LocalResourceFile), Consts.DateRangeLastMonth))
            If ShowCustom Then
                Me.cmbDateRange.Items.Add(New ListItem(Localization.GetString(Consts.DateRangeCustom, LocalResourceFile), Consts.DateRangeCustom))
            End If
        End Sub

        Private Sub BindInteval()
            If TrInterval.Visible Then
                Me.RBLInterval.Items.Clear()

                For Each objInterval As Interval In [Enum].GetValues(GetType(Interval))
                    Dim item As ListItem = New ListItem(Localization.GetString(objInterval.ToString, LocalResourceFile), objInterval.ToString())
                    RBLInterval.Items.Add(item)
                Next
            End If
        End Sub

        Public Sub RefreshInterval()

            Select Case _Interval
                Case Interval.Hour
                    Me.RBLInterval.Items(0).Selected = True
                Case Interval.Year
                    Me.RBLInterval.Items(1).Selected = True
                Case Interval.Month
                    Me.RBLInterval.Items(2).Selected = True
                Case Interval.Day
                    Me.RBLInterval.Items(3).Selected = True
                Case Interval.None
                    Me.RBLInterval.Items(4).Selected = True
                Case Else
                    Select Case _DateRange
                        Case Consts.DateRangeAllTime
                        Case Consts.DateRangeCustom
                            Me.RBLInterval.Items(1).Selected = True
                        Case Consts.DateRangeToday
                        Case Consts.DateRangeYesterday
                            Me.RBLInterval.Items(0).Selected = True
                        Case Consts.DateRangeLast7Days
                        Case Consts.DateRangeLastWeek
                        Case Consts.DateRangeLastMonth
                            Me.RBLInterval.Items(3).Selected = True
                        Case Else
                            Me.RBLInterval.Items(1).Selected = True
                    End Select
            End Select
        End Sub

        ''' <summary>
        ''' This javascript function registered here, controls the show and hide
        ''' depending on the selected range type selected, of the available intervals
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterOnDateRangeChange()
            Dim cmbDateRangeOnChangePart1 As String = String.Empty
            Dim cmbDateRangeOnChangePart2 As String = String.Empty

            If ShowInterval Then
                Dim script As String = "function ShowHideIntervals (inicio) {" &
                                    "   var cmbDate = document.getElementById('" & cmbDateRange.ClientID & "'); " &
                                    "   var table = document.getElementById('" & RBLInterval.ClientID & "');" &
                                    "   var rows = table.getElementsByTagName('tr');" &
                                    "   switch (cmbDate.options[cmbDate.selectedIndex].value){ " &
                                    "           case '" & Consts.DateRangeCustom & "':" &
                                    "           case '" & Consts.DateRangeAllTime & "':" &
                                    "               rows[0].style.display = 'none';" &
                                    "               rows[1].style.display = '';" &
                                    "               rows[2].style.display = '';" &
                                    "               rows[3].style.display = '';" &
                                    "               rows[4].style.display = '';" &
                                    "               if (inicio == 'false'){ " &
                                    "                   rows[1].getElementsByTagName('input')[0].checked='checked';" &
                                    "               }" &
                                    "           break;" &
                                    "           case '" & Consts.DateRangeToday & "':" &
                                    "           case '" & Consts.DateRangeYesterday & "':" &
                                    "               rows[0].style.display = '';" &
                                    "               rows[1].style.display = 'none';" &
                                    "               rows[2].style.display = 'none';" &
                                    "               rows[3].style.display = 'none';" &
                                    "               rows[4].style.display = '';" &
                                    "               if (inicio == 'false'){ " &
                                    "                   rows[0].getElementsByTagName('input')[0].checked='checked';" &
                                    "               }" &
                                    "           break;" &
                                    "           case '" & Consts.DateRangeLast7Days & "':" &
                                    "           case '" & Consts.DateRangeLastWeek & "':" &
                                    "           case '" & Consts.DateRangeLastMonth & "':" &
                                    "           case '" & Consts.DateRangeThisWeek & "':" &
                                    "           case '" & Consts.DateRangeThisMonth & "':" &
                                    "           case '" & Consts.DateRangeLast30Days & "':" &
                                    "               rows[0].style.display = 'none';" &
                                    "               rows[1].style.display = 'none';" &
                                    "               rows[2].style.display = 'none';" &
                                    "               rows[3].style.display = '';" &
                                    "               rows[4].style.display = '';" &
                                    "               if (inicio == 'false'){ " &
                                    "                   rows[3].getElementsByTagName('input')[0].checked='checked';" &
                                    "               }" &
                                    "           break;" &
                                    "       } " &
                                    "   } "

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DnneLeaening_ShowOrHideIntervals", script, True)

                Page.ClientScript.RegisterStartupScript(Me.GetType, "IZMLMS_onloadShowOrHideIntervals", "ShowHideIntervals('true');", True)

                cmbDateRangeOnChangePart1 = "   ShowHideIntervals('false'); "
            End If

            Dim ltimeSpan As TimeSpan
            ltimeSpan = New TimeSpan(7, 0, 0, 0)

            cmbDateRangeOnChangePart2 = "   var trStartDate = document.getElementById('" & TrStartDate.ClientID & "'); " &
                                        "   var trEndDate = document.getElementById('" & TrEndDate.ClientID & "'); " &
                                        "   var cmbDate = document.getElementById('" & cmbDateRange.ClientID & "'); " &
                                        "   var validator = document.getElementById('" & valDateRange.ClientID & "');" &
                                        "       switch (cmbDate.options[cmbDate.selectedIndex].value){ " &
                                        "           case '" & Consts.DateRangeCustom & "':" &
                                        "               trStartDate.style.display = '';" &
                                        "               trEndDate.style.display = '';" &
                                        "               document.getElementById('" & tblRangeValidator.ClientID & "').style.display = '';" &
                                        "               ValidatorEnable(validator, true);" &
                                        "           break;" &
                                        "           default:" &
                                        "               trStartDate.style.display = 'none';" &
                                        "               trEndDate.style.display = 'none';" &
                                        "               document.getElementById('" & tblRangeValidator.ClientID & "').style.display = 'none';" &
                                        "               ValidatorEnable(validator, false);" &
                                        "           break;" &
                                        "       }; "

            'This is executed only when you change the dropdown, not when the page load, to avoid lost user selection
            'but if the user change to another dropdown value, and then get back to custom, the values are reset to defaults
            Dim cmbDateRangeOnChangePart3 As String = "   $find('" & rdpStartDate.ClientID & "').set_selectedDate(new Date());" &
                                        "   $find('" & rdpEndDate.ClientID & "').set_selectedDate(new Date());"

            'Register startup script because after select Generate Report you can't Refresh the Report.
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DnneLeaening_RefreshCboDates", "function RefreshCboDates () { try{ " & cmbDateRangeOnChangePart2 & "} catch(err) {} } ", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "DnneLeaening_onloadRefreshCboDates", "RefreshCboDates();", True)

            cmbDateRange.Attributes.Add("onChange", cmbDateRangeOnChangePart1 & cmbDateRangeOnChangePart2 & cmbDateRangeOnChangePart3)
        End Sub

#End Region

#Region "Event Handlers"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            RegisterOnDateRangeChange()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try

                LocalizeLabels()

                If Not Page.IsPostBack Then

                    If DateRange = Consts.DateRangeCustom Then
                        TrEndDate.Style.Item("display") = String.Empty
                        TrStartDate.Style.Item("display") = String.Empty
                    End If

                    If _DateRange = Null.NullString Or _DateRange <> Consts.DateRangeCustom Then
                        TrEndDate.Style.Clear()
                        TrEndDate.Style.Add("display", "none")
                        TrStartDate.Style.Clear()
                        TrStartDate.Style.Add("display", "none")
                    End If

                    If cmbDateRange.Items.Count = 0 Then
                        Me.LoadDateRange()
                    End If

                    If (_DateRange <> Consts.DateRangeCustom) AndAlso (rdpStartDate.SelectedDate IsNot Nothing And rdpEndDate.SelectedDate IsNot Nothing) Then
                        Dim ltimeSpan As TimeSpan
                        ltimeSpan = New TimeSpan(7, 0, 0, 0)
                        rdpStartDate.SelectedDate = TimeZoneUtils.GetPortalTodayDate().Subtract(ltimeSpan)
                        rdpEndDate.SelectedDate = New DateTime(TimeZoneUtils.GetPortalTodayDate().Year, TimeZoneUtils.GetPortalTodayDate().Month, TimeZoneUtils.GetPortalTodayDate().Day, 23, 59, 59)
                    End If

                    If Not ShowInterval Then
                        Me.TrInterval.Visible = False
                    Else
                        Me.TrInterval.Visible = True
                        BindInteval()
                        RefreshInterval()
                    End If
                End If

                If DateRange = Consts.DateRangeCustom Then
                    TrStartDate.Attributes.Remove("style")
                    TrStartDate.Attributes.Add("style", "display:''")
                    TrEndDate.Attributes.Remove("style")
                    TrEndDate.Attributes.Add("style", "display:''")
                Else
                    TrStartDate.Attributes.Remove("style")
                    TrStartDate.Attributes.Add("style", "display:none")
                    TrEndDate.Attributes.Remove("style")
                    TrEndDate.Attributes.Add("style", "display:none")
                End If
            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

#End Region

    End Class

End Namespace