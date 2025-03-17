open System

let add (add: char) (sSeq: seq<string>) : seq<string> =
    sSeq |> Seq.map (fun str -> str + string add)


let MySeq : seq<string> = 
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
