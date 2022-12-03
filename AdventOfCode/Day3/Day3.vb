Public Class Day3
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day3\input.txt")
        Dim runningTotal As Integer = 0
        For Each line As String In lines
            Dim firstHalf As String = line.Substring(0, line.Length / 2)
            Dim secondHalf As String = line.Substring(line.Length / 2)
            Dim itemsSet As HashSet(Of Char) = New HashSet(Of Char)
            For Each item As Char In firstHalf
                itemsSet.Add(item)
            Next
            For Each item As Char In secondHalf
                If itemsSet.Contains(item) Then
                    runningTotal += scoreFromItem(item)
                    Exit For
                End If
            Next

        Next
        Console.WriteLine(runningTotal)
    End Sub

    Public Shared Sub calculate2()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day3\input.txt")
        Dim runningTotal As Integer = 0
        Dim currentLine = 0
        While currentLine < lines.Length
            For Each item As Char In lines(currentLine)
                If Not lines(currentLine + 1).Contains(item) Then
                    Continue For
                End If
                If Not lines(currentLine + 2).Contains(item) Then
                    Continue For
                End If
                runningTotal += scoreFromItem(item)
                Console.WriteLine(item)
                Exit For
            Next
            currentLine += 3
        End While
        Console.WriteLine(runningTotal)
    End Sub

    Public Shared Function scoreFromItem(item As Char) As Integer
        If item <= "Z"c Then
            Return (Asc(item) - Asc("A"c)) + 27
        End If
        Return (Asc(item) - Asc("a"c)) + 1
    End Function


End Class
