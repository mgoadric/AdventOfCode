package day1;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.Collections;
import java.util.NoSuchElementException;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {

        try {
            File file = new File("src/day1/data/input.txt");
            Scanner scanner = new Scanner(new BufferedReader(new FileReader(file)));

            ArrayList<Integer> sums = new ArrayList<>();
            int current = 0;
            while (scanner.hasNext()) {
                String line = scanner.nextLine();
                if (line.length() == 0) {
                    sums.add(current);
                    System.out.println("Adding " + current);
                    current = 0;
                } else {
                    current += Integer.parseInt(line);
                }
            }
            sums.add(current);

            // https://www.baeldung.com/java-collection-min-max
            Integer max = sums
                    .stream()
                    .mapToInt(v -> v)
                    .max().orElseThrow(NoSuchElementException::new);

            System.out.println("Part 1: " + max);

            // https://www.javatpoint.com/how-to-sort-arraylist-in-java
            sums.sort(Collections.reverseOrder());

            System.out.println("Part 2: " + (sums.get(0) +
                    sums.get(1) + sums.get(2)));

        } catch (FileNotFoundException fnfe) {

        }

    }
}
