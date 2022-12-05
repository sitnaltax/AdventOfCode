Public Class Day5
    Public Shared Sub calculate()
        Dim stacks As List(Of Stack(Of Char)) = New List(Of Stack(Of Char))
        For i As Integer = 0 To 9
            stacks.Add(New Stack(Of Char))
        Next

        Dim inputs() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day5\input-setup.txt")
        For Each inputLine As String In inputs.Reverse
            For i As Integer = 1 To 9
                Dim target As Char = inputLine(1 + (i - 1) * 4)
                If target >= "A"c Then
                    stacks(i).Push(target)
                End If
            Next
            Console.WriteLine()
        Next

        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day5\input.txt")
        For Each line As String In lines
            Dim tokens() As String = line.Split(" ")
            Dim number As Integer = Convert.ToInt32(tokens(1))
            Dim source As Integer = Convert.ToInt32(tokens(3))
            Dim destination As Integer = Convert.ToInt32(tokens(5))
            For ix As Integer = 1 To number
                Dim box As Char = stacks(source).Pop()
                stacks(destination).Push(box)
            Next
        Next

        For Each stack As Stack(Of Char) In stacks
            If stack.Count > 0 Then
                Console.Write(stack.Peek())
            End If
        Next
    End Sub

    Public Shared Sub calculate2()
        Dim stacks As List(Of Stack(Of Char)) = New List(Of Stack(Of Char))
        For i As Integer = 0 To 9
            stacks.Add(New Stack(Of Char))
        Next

        Dim inputs() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day5\input-setup.txt")
        For Each inputLine As String In inputs.Reverse
            For i As Integer = 1 To 9
                Dim target As Char = inputLine(1 + (i - 1) * 4)
                If target >= "A"c Then
                    stacks(i).Push(target)
                End If
            Next
            Console.WriteLine()
        Next

        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day5\input.txt")
        For Each line As String In lines
            Dim tokens() As String = line.Split(" ")
            Dim number As Integer = Convert.ToInt32(tokens(1))
            Dim source As Integer = Convert.ToInt32(tokens(3))
            Dim destination As Integer = Convert.ToInt32(tokens(5))
            Dim tempStack As Stack(Of Char) = New Stack(Of Char)
            For ix As Integer = 1 To number
                Dim box As Char = stacks(source).Pop()
                tempStack.Push(box)
            Next
            For ix As Integer = 1 To number
                Dim box As Char = tempStack.Pop()
                stacks(destination).Push(box)
            Next
        Next

        For Each stack As Stack(Of Char) In stacks
            If stack.Count > 0 Then
                Console.Write(stack.Peek())
            End If
        Next
    End Sub


End Class
