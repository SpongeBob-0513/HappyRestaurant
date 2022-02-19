using Common;

namespace Server.Controller
{
    // 抽象 abstract 不能被实例化，也就是说不会有这种类的实例，相当于一个提示作用
    // 虚拟 virtual 
    // 抽象方法只有声明没有实现,需要在子类中实现；虚拟方法有声明和实现,并且可以在子类中覆盖,也可以不覆盖使用父类的默认实现
    /// <summary>
    /// 处理客户端发送过来的数据  默认方法  data 已经在 Massage 中解析好的数据
    /// 返回给客户端数据
    /// 虚拟 子类可实现可不实现
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    abstract public class BaseController
    {
        private RequestCode _requestCode = RequestCode.None;

        public RequestCode RequestCode => _requestCode;
        
        public virtual string DefaultHandle(string data)
        {
            return null; // 默认返回空
        } 
    }
}