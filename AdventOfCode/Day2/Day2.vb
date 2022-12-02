Imports System.IO
Public Class Day2

    REM combinations where you win
    Shared victoryDict As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
        {"A", "Y"},
        {"B", "Z"},
        {"C", "X"}
        }

    REM combinations where you tie
    Shared drawDict As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
        {"A", "X"},
        {"B", "Y"},
        {"C", "Z"}
        }

    REM combinations where you tie
    Shared lossDict As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
        {"A", "Z"},
        {"B", "X"},
        {"C", "Y"}
        }
    Public Shared Sub calculate()
        Dim choiceScore As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
        choiceScore.Add("X", 1)
        choiceScore.Add("Y", 2)
        choiceScore.Add("Z", 3)

        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day2\input.txt")
        Dim runningTotal As Integer = 0
        For Each line As String In lines
            Dim parts() As String = line.Split(" ")
            Dim opponentChoice As String = parts(0)
            REM Dim myChoice As String = parts(1)
            Dim myChoice As String = choiceFromRequiredResult(parts(0), parts(1))
            runningTotal += choiceScore(myChoice)
            If victoryDict(opponentChoice) = myChoice Then
                runningTotal += 6
            ElseIf drawDict(opponentChoice) = myChoice Then
                runningTotal += 3
            Else
                runningTotal += 0
            End If
        Next
        Console.WriteLine(runningTotal)
    End Sub

    Public Shared Function choiceFromRequiredResult(opponentChoice As String, requiredResult As String) As String
        Select Case requiredResult
            Case "X"
                Return lossDict(opponentChoice)
            Case "Y"
                Return drawDict(opponentChoice)
            Case "Z"
                Return victoryDict(opponentChoice)
            Case Else
                Console.WriteLine("Unexpected Value")
                Return "X"
        End Select
    End Function
End Class
