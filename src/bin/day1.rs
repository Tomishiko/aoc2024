use std::io::BufRead;
use std::fs;
fn main() {
// Part 1 ############################################################
    let file = fs::File::open("./input/day1").unwrap();
    let lines = std::io::BufReader::new(file).lines();
    let mut left:Vec<u32> = Vec::with_capacity(1000);
    let mut right:Vec<u32> = Vec::with_capacity(1000);
    let start = std::time::Instant::now();
    for line in lines.flatten(){
        let tempLine:Vec<&str> = line.split("   ").collect();
        match tempLine[0].parse() {
           Ok(val) =>{
                left.push(val);

            }
            Err(_)=>{}
        }
        match tempLine[1].parse() {
           Ok(val) =>{
                right.push(val);

            }
            Err(_)=>{}
        }

    }
    left.sort();
    right.sort();

    let mut answer:u32=0;
    for i in 0..right.len(){
            answer += right[i].abs_diff(left[i]);

    }

// Part 2 #############################################################
    let mut score = 0;
    let mut indexL = 0;
    let mut indexR = 0;
    let mut repL = 1;
    let mut repR = 1;
    let len = left.len();
// kinda merge sort

        //for i in 0..len{
        //    println!("{}  {}",left[i],right[i]);
        //}

    while indexL< len && indexR<len {
        //println!("indexL {indexL}, indexR {indexR}, score {score}, repL {repL}, repR {repR}");
        if indexL < len - 1   && left[indexL + 1] == left[indexL]{
            indexL += 1;
            repL +=1;
            continue;
        }
        if indexR < len - 1 && right[indexR + 1] == right[indexR]{
            indexR += 1;
            repR += 1;
            continue;
        }

        //
        //old stuff
        //
        if left[indexL] < right[indexR] {
            indexL += 1;
            repL = 1;
        } else if left[indexL] > right[indexR] {
            indexR += 1;
            repR = 1;
        }

        // If equal, add to result and move both
        else {
            score += left[indexL] * repR * repL;
            indexL += 1;
            indexR += 1;
            repL = 1;
            repR = 1;
        }

    }
    let finish = start.elapsed();
    println!("time: {:.5?}",finish);
    println!("Part 1: {answer}");
    println!("Part 2: {score}");

}
