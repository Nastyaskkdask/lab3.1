open System

let rnd = new Random()

let generateRandomString (minL: int) (maxL: int) =
    let length = rnd.Next(minL, maxL + 1)
    [| for i in 0..length - 1 do
        let charType = rnd.Next(3)
        yield
            match charType with
            | 0 -> char (rnd.Next(10) + int '0')
            | 1 -> char (rnd.Next(26) + int 'a')
            | 2 -> char (rnd.Next(26) + int 'A')
            | _ -> ' '
    |] |> String

let generateRand (count: int) (minL: int) (maxL: int) : seq<string> = 
    seq { for i in 1..count do
            yield generateRandomString minL maxL }

let add (add: char) (sSeq: seq<string>) : seq<string> =
    sSeq |> Seq.map (fun str -> str + string add)

printf "Выберите способ ввода списка строк (1 - рандомный, 2 - ручной): "
let inputMode = Console.ReadLine()

let MySeq : seq<string> = 
    match inputMode with
    | "1" ->
        printf "Введите кол-во строк: "
        let size = Console.ReadLine()
        if size = "0" then
            printfn "\nСписок пуст."
            exit 1
        match System.Int32.TryParse(size) with
        | true, parsedInt ->
            let minL = 3
            let maxL = 7
            generateRand parsedInt minL maxL
        | false, _ ->
            printfn "Ошибка: Введите целое число."
            exit 1
    | "2" ->
        printf "Введите строки:\n"
        let rec readLines acc =
            match Console.ReadLine() with
            | null | "" -> acc |> Seq.toList |> List.rev |> Seq.ofList // Конвертация списка в последовательность
            | line -> readLines (Seq.append acc (seq { yield line })) // Добавляем строку в последовательность

        let lines = readLines Seq.empty
        if Seq.isEmpty lines then
            printfn "\nСписок пуст."
            exit 1
        else
            lines
    | _ ->
        printfn "Ошибка: Некорректный выбор режима ввода."
        exit 1


printf "Введите символ для добавления в конец: "
let symbol = Console.ReadLine()

let check2 =
    if String.IsNullOrEmpty(symbol) then
        printfn "Ошибка: Вы не ввели символ."
        exit 1
    elif symbol.Length > 1 then
        printfn "Ошибка: Введите только один символ."
        exit 1
    else
        symbol.[0]

printfn "Исходный список:"
MySeq |> Seq.iter (printfn "%s")

let newList = add check2 MySeq
printfn "\nНовый список:"
newList |> Seq.iter (printfn "%s")