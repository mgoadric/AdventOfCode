import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class DayOne {

    public static void main(String[] args) {

        int count = 0;
        // https://www.journaldev.com/709/java-read-file-line-by-line

        try {
            BufferedReader br = new BufferedReader(
                    new FileReader("input1.txt"));
            String line = br.readLine();
            int[] window = new int[3];
            boolean full = false;
            int current = 0;

            while (line != null) {
                int value = Integer.parseInt(line);
                if (full) {
                    int newsum = 0;
                    for (int i = 0; i < 3; i++) {
                        if (i != current) {
                            newsum += window[i];
                        } else {
                            newsum += value;
                        }
                    }
                    if (newsum > window[0] + window[1] + window[2]) {
                        count++;
                    }
                }

                window[current] = value;
                current++;
                if (current >= 3) {
                    current = 0;
                    full = true;
                }

                line = br.readLine();
            }
            br.close();

        } catch (IOException ioe) {
            ioe.printStackTrace();
        }
        // spit out the answer
        System.out.println(count);
    }
}
