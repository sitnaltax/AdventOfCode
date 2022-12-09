Public Class Day9
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day9\input.txt")
        Dim moves As List(Of Move) = New List(Of Move)
        For Each line As String In lines
            Dim tokens() As String = line.Split(" ")
            moves.Add(New Move(tokens(0)(0), tokens(1)))
        Next

        Console.WriteLine(calculatePositions(moves))
    End Sub

    Const TAILS_COUNT As Integer = 9

    Private Shared Function calculatePositions(moves As List(Of Move)) As Integer
        Dim previousLocations As HashSet(Of Integer) = New HashSet(Of Integer)
        Dim headCoords As Coords = New Coords(0, 0)
        Dim tails As List(Of Coords) = New List(Of Coords)
        For i As Integer = 1 To TAILS_COUNT
            tails.Add(New Coords(0, 0))
        Next

        Dim iter As Integer = 0
        For Each move As Move In moves
            iter = iter + 1
            For i As Integer = 1 To move.Count
                Select Case move.Direction
                    Case "R"
                        headCoords.X += 1
                    Case "L"
                        headCoords.X -= 1
                    Case "U"
                        headCoords.Y += 1
                    Case "D"
                        headCoords.Y -= 1
                End Select
                For j As Integer = 0 To TAILS_COUNT - 1
                    If j = 0 Then
                        tails(j) = moveTail(headCoords, tails(j))
                    Else
                        tails(j) = moveTail(tails(j - 1), tails(j))
                    End If
                Next
                previousLocations.Add(HashFromPosition(tails(TAILS_COUNT - 1)))
            Next
        Next
        Return previousLocations.Count
    End Function

    Private Shared Function moveTail(headCoords As Coords, tailCoords As Coords) As Coords
        If (Math.Abs(headCoords.X - tailCoords.X) < 2 And Math.Abs(headCoords.Y - tailCoords.Y) < 2) Then
            Return tailCoords
        End If
        If (Math.Abs(headCoords.X - tailCoords.X) = 2 And Math.Abs(headCoords.Y - tailCoords.Y) = 2) Then
            ' Tail keeps up
            Return New Coords((headCoords.X + tailCoords.X) / 2, (headCoords.Y + tailCoords.Y) / 2)
        End If
        If Math.Abs(headCoords.X - tailCoords.X) = 2 Then
            ' Tail keeps up
            Return New Coords((headCoords.X + tailCoords.X) / 2, headCoords.Y)
        End If
        If Math.Abs(headCoords.Y - tailCoords.Y) = 2 Then
            ' Tail keeps up
            Return New Coords(headCoords.X, (headCoords.Y + tailCoords.Y) / 2)
        End If
        Console.WriteLine("should not be here")
        Return New Coords(0, 0)
    End Function

    Private Shared Function HashFromPosition(loc As Coords) As Integer
        Return loc.X + loc.Y * 100000
    End Function

    Public Class Coords
        Public X As Integer
        Public Y As Integer
        Public Sub New(xx As Integer, yy As Integer)
            X = xx
            Y = yy
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("({0}, {1})", X, Y)
        End Function
    End Class

    Public Class Move
        Public Direction As Char
        Public Count As Integer
        Public Sub New(d As Char, c As Integer)
            Direction = d
            Count = c
        End Sub
    End Class

End Class
