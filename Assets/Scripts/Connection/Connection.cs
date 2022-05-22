public class Connection {
    private IConnectable src, target;

    public Connection(IConnectable src, IConnectable target) {
        this.src = src;
        this.target = target;

        src.OnConnect(target);
        target.OnConnect(src);
    }
}
