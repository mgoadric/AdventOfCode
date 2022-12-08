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

    public int AllFileSize(List<int> smalls) {
        int allsize = size;
        foreach (FileDir f in files.Values) {
            allsize += f.AllFileSize(smalls);
        }
        if (size == 0) {
            smalls.Add(allsize);
        }
        return allsize;
    }


}