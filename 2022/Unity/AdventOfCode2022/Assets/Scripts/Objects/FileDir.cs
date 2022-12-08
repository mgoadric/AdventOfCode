using System.Collections.Generic;

public class FileDir {

    private int size;
    public Dictionary<string, FileDir> files;

    public FileDir parent;

    public FileDir(int size, FileDir parent) {
        this.parent = parent;
        this.size = size;
        files = new Dictionary<string, FileDir>();
    }

    public int FileSize() {
        int allsize = size;
        foreach (FileDir f in files.Values) {
            allsize += f.FileSize();
        }
        return allsize;
    }

    public int LimitedFileSize(List<int> smalls) {
        int allsize = size;
        foreach (FileDir f in files.Values) {
            allsize += f.LimitedFileSize(smalls);
        }
        if (size == 0 && allsize <= 100000) {
            smalls.Add(allsize);
        }
        return allsize;
    }


}