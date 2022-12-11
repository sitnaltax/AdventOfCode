Public Class Day11

    Const WORRY_DIV As Int64 = 19 * 3 * 11 * 17 * 5 * 2 * 13 * 7
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day11\input.txt")
        For Each line As String In lines

        Next

        Dim Monkeys As List(Of Monkey) = New List(Of Monkey)
        Monkeys.Add(New Monkey("0", {75L, 75, 98, 97, 79, 97, 64}.ToList(), Function(x) x * 13, Function(x) x Mod 19 = 0))
        Monkeys.Add(New Monkey("1", {50L, 99, 80, 84, 65, 95}.ToList(), Function(x) x + 2, Function(x) x Mod 3 = 0))
        Monkeys.Add(New Monkey("2", {96L, 74, 68, 96, 56, 71, 75, 53}.ToList(), Function(x) x + 1, Function(x) x Mod 11 = 0))
        Monkeys.Add(New Monkey("3", {83L, 96, 86, 58, 92}.ToList(), Function(x) x + 8, Function(x) x Mod 17 = 0))
        Monkeys.Add(New Monkey("4", {99L}.ToList(), Function(x) x * x, Function(x) x Mod 5 = 0))
        Monkeys.Add(New Monkey("5", {60L, 54, 83}.ToList(), Function(x) x + 4, Function(x) x Mod 2 = 0))
        Monkeys.Add(New Monkey("6", {77L, 67}.ToList(), Function(x) x * 17, Function(x) x Mod 13 = 0))
        Monkeys.Add(New Monkey("7", {95L, 65, 58, 76}.ToList(), Function(x) x + 5, Function(x) x Mod 7 = 0))
        Monkeys(0).SetTargets(Monkeys(2), Monkeys(7))
        Monkeys(1).SetTargets(Monkeys(4), Monkeys(5))
        Monkeys(2).SetTargets(Monkeys(7), Monkeys(3))
        Monkeys(3).SetTargets(Monkeys(6), Monkeys(1))
        Monkeys(4).SetTargets(Monkeys(0), Monkeys(5))
        Monkeys(5).SetTargets(Monkeys(2), Monkeys(0))
        Monkeys(6).SetTargets(Monkeys(4), Monkeys(1))
        Monkeys(7).SetTargets(Monkeys(3), Monkeys(6))

        Dim TestMonkeys As List(Of Monkey) = New List(Of Monkey)
        TestMonkeys.Add(New Monkey("0", {79L, 98}.ToList(), Function(x) x * 19, Function(x) x Mod 23 = 0))
        TestMonkeys.Add(New Monkey("1", {54L, 65, 75, 74}.ToList(), Function(x) x + 6, Function(x) x Mod 19 = 0))
        TestMonkeys.Add(New Monkey("2", {79L, 60, 97}.ToList(), Function(x) x * x, Function(x) x Mod 13 = 0))
        TestMonkeys.Add(New Monkey("3", {74L}.ToList(), Function(x) x + 3, Function(x) x Mod 17 = 0))
        TestMonkeys(0).SetTargets(TestMonkeys(2), TestMonkeys(3))
        TestMonkeys(1).SetTargets(TestMonkeys(2), TestMonkeys(0))
        TestMonkeys(2).SetTargets(TestMonkeys(1), TestMonkeys(3))
        TestMonkeys(3).SetTargets(TestMonkeys(0), TestMonkeys(1))


        For i As Int64 = 1 To 10000
            For Each monkey As Monkey In Monkeys
                monkey.ProcessAllItems()
            Next
            'Console.WriteLine(String.Format("After Round {0}", i))
        Next
        For Each monkey As Monkey In Monkeys
            Console.WriteLine(String.Format("Monkey {0}: inspected {1}", monkey.Name, monkey.Inspections))
        Next




    End Sub

    Public Class Monkey
        Public Name As String
        Public Inventory As List(Of Int64)
        Public Operation As Func(Of Int64, Int64)
        Public Test As Predicate(Of Int64)
        Public TargetTrue As Monkey
        Public TargetFalse As Monkey
        Public Inspections As Int64

        Public Sub New(n As String, i As List(Of Int64), o As Func(Of Int64, Int64), t As Predicate(Of Int64))
            Name = n
            Inventory = i
            Operation = o
            Test = t
            Inspections = 0
        End Sub

        Public Sub SetTargets(tt As Monkey, tf As Monkey)
            TargetTrue = tt
            TargetFalse = tf
        End Sub

        Public Sub ProcessAllItems()
            While Inventory.Count > 0
                ProcessItem()
            End While
        End Sub

        Public Sub ProcessItem()
            Dim currentItem As Int64 = Inventory(0)
            'Console.WriteLine(String.Format("Monkey {0} inspecting item {1}", Name, currentItem))

            Inventory.RemoveAt(0)
            ' Inspect it
            currentItem = Operation(currentItem)
            Inspections += 1
            ' Worry decay
            currentItem = currentItem Mod WORRY_DIV
            If Test(currentItem) Then
                'Console.WriteLine(String.Format("Monkey {0} passing item {1} to {2}", Name, currentItem, TargetTrue.Name))
                TargetTrue.Inventory.Add(currentItem)
            Else
                'Console.WriteLine(String.Format("Monkey {0} passing item {1} to {2}", Name, currentItem, TargetFalse.Name))
                TargetFalse.Inventory.Add(currentItem)
            End If
        End Sub

    End Class

End Class
