use regex::Regex;
use std::collections::VecDeque;
use std::fs::File;
use std::io::{BufRead, BufReader};

pub struct DataFile {
    #[allow(dead_code)]
    pub pathname: String,
    pub lines: VecDeque<Vec<i32>>,
}

impl DataFile {
    pub fn new(pathname: &str) -> Self {
        Self {
            pathname: pathname.to_string(),
            lines: DataFile::read_lines(pathname),
        }
    }

    fn read_lines(pathname: &str) -> VecDeque<Vec<i32>> {
        let mut result: VecDeque<Vec<i32>> = VecDeque::new();
        let f = File::open(pathname).expect("Unable to open file");
        let f = BufReader::new(f);
        for line in f.lines() {
            let line = line.expect("Unable to read line");
            let parsed_line: Vec<i32> = DataFile::parse_line(line);
            result.push_back(parsed_line);
        }
        result
    }

    fn parse_line(line: String) -> Vec<i32> {
        let mut result: Vec<i32> = Vec::new();
        let re = Regex::new(r"\d+").unwrap();
        for mat in re.find_iter(line.as_str()) {
            let number = &line[mat.start()..mat.end()];
            let number: i32 = number.parse().expect("Failed to parse string");
            result.push(number);
        }
        result
    }
}
