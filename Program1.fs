open System

let add (add: char) (sSeq: seq<string>) : seq<string> =
    printfn "Функция 'add' вызвана(отложенное вычисление)."
    sSeq |> Seq.map (fun str ->
        printfn "Обработка строки: %s" str
        str + string add)


let getMySeq() : seq<string> =
    printfn "Введите строки (пустая строка для завершения):"
    let rec readLines acc =
        match Console.ReadLine() with
        | null | "" ->
            acc
        | line ->
            readLines (Seq.append acc (seq { yield line }))

    readLines Seq.empty

let mySeq : seq<string> = getMySeq()


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


printfn "Вызываем функцию 'add'.  Вычисление отложено."
let newList = add check2 mySeq

printfn "\nИсходный список:"
mySeq |> Seq.iter (printfn "%s")

printfn "\nНовый список:"
newList |> Seq.iter (printfn "%s")

