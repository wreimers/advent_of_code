mod day_01;

fn main() {
    // let f = File::open("./var/day_01_sample_input.txt").expect("Unable to open file");
    // let f = File::open("./var/day_01_input.txt").expect("Unable to open file");
    // let f = File::open("./var/day_01_mmitton_input.txt").expect("Unable to open file");

    let mut df = day_01::DataFile::new("./var/day_01_input.txt");

    let mut sum = 0;
    while df.left.len() > 0 {
        let left = df.left.pop().unwrap();
        let right = df.right.pop().unwrap();
        sum += (right - left).abs();
    }

    println!("{}", sum);
}
