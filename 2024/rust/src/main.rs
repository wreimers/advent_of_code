mod day_01;

fn main() {
    main_day_01_part_02();
}

fn main_day_01_part_02() {
    let sum = day_01::part_2("./var/day_01_input.txt");
    println!("{}", sum);
}

fn main_day_01_part_01() {
    let sum = day_01::part_1("./var/day_01_input.txt");
    println!("{}", sum);
}
