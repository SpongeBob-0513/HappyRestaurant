using Common;

namespace Server.Controller
{
    // 抽象 abstract 不能被实例化，也就是说不会有这种类的实例，相当于一个提示作用
    // 虚拟 virtual 
    // 抽象方法只有声明没有实现,需要在子类中实现；虚拟方法有声明和实现,并且可以在子类中覆盖,也可以不覆盖使用父类的默认实现
    abstract public class BaseController
    {
        private RequestCode _requestCode = RequestCode.None;

        public virtual void DefaultHandle(){}
    }
}