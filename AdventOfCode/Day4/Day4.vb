Public Class Day4
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day4\input.txt")
        Dim runningTotal As Integer = 0
        For Each line As String In lines
            Dim sections() As String = line.Split(",")
            Dim Section1 As Section = Section.FromString(sections(0))
            Dim Section2 As Section = Section.FromString(sections(1))
            If (SectionContainsEitherEnd(Section1, Section2) Or SectionContainsEitherEnd(Section2, Section1)) Then
                ' (SectionContains(Section1, Section2) Or SectionContains(Section2, Section1)) Then
                runningTotal += 1
            End If
        Next
        Console.WriteLine(runningTotal)
    End Sub

    Private Class Section
        Public First As Integer
        Public Last As Integer

        Public Sub New(f As Integer, l As Integer)
            First = f
            Last = l
        End Sub

        Public Shared Function FromString(input As String) As Section
            Dim parts() As String = input.Split("-")
            Return New Section(Convert.ToInt32(parts(0)), Convert.ToInt32(parts(1)))
        End Function
    End Class

    Private Shared Function SectionContains(a As Section, b As Section) As Boolean
        Return (a.First <= b.First) And (a.Last >= b.Last)
    End Function

    Private Shared Function SectionContainsEitherEnd(a As Section, b As Section) As Boolean
        Return ((a.First <= b.First) And (a.Last >= b.First)) Or ((a.First <= b.Last) And (a.Last >= b.Last))
    End Function

End Class
