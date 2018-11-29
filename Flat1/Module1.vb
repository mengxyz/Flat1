Imports System.Data
Imports System.Data.SqlClient
Module Module1
    Public strConn As String = "user id = sa; password=1234; data source=MENGXANANTASAK; initial catalog=db_Final_Project_1;"
    'Public strConn As String = "user id = sa ; password = 1234 ; data source = MENGXANANTASAK; intitail catalog = db_Final_Project_1;"
    Public Conn As New SqlConnection
    Public User_Na As String = 122
    Public User_Status As String = 0

    Public Sub Connect()
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.ConnectionString = strConn
        Conn.Open()
    End Sub
End Module
