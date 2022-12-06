Public Class Day6
    Public Shared Sub calculate()
        Dim inputs() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day6\input.txt")
        Dim Line As String = inputs(0)
        Const SIZE = 14
        Dim buffer(SIZE - 1) As Char
        For i As Integer = 0 To SIZE - 1
            buffer(i) = Line(0)
        Next
        For i As Integer = 0 To Line.Count - 1
            buffer(i Mod SIZE) = Line(i)
            Dim hash As HashSet(Of Char) = New HashSet(Of Char)
            For Each entry As Char In buffer
                hash.Add(entry)
            Next
            If hash.Count = SIZE Then
                Console.WriteLine(i + 1)
                Exit For
            End If
        Next
    End Sub
End Class
