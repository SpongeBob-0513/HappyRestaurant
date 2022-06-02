# HappyRestaurant

#### 介绍
毕业设计（基于unity的餐厅经营游戏）
本毕业设计就采用Unity引擎作为工具，开发了一款可以联机的餐厅经营游戏。在整体游戏的开发过程中，代码的整体结构和代码的具体实现都非常重要。游戏共创建了服务端、客户端、共享工程三个工程，服务端负责管理数据库，根据客户端的需求进行数据的计算和转发，客户端则根据服务端发送的数据来控制游戏中的数据变化，共享工程则是用来存放客户端和服务端共同使用的方法和变量。三个项目互相配合，实现了一款可以联机的餐厅经营游戏。
为了实现联机功能，服务端作为中介，负责接收客户端的状态并发送给房间内的其他客户端，实现房间内所有客户端之间的信息同步，以实现联机的效果。
在游戏的框架设计中，核心思想是用静态类来存放公用的变量和方法，也称“单例模式”。如果需要类与类之间方法的调用，则使用这个静态类作为中介，将方法放入静态类中，通过使用静态类来进行调用，这种做法大大降低了代码的耦合性，减少了代码结构调整时的工作量，代码的结构也更加整洁。

#### 软件架构
软件架构说明


#### 安装教程

1.  xxxx
2.  xxxx
3.  xxxx

#### 使用说明

1.  xxxx
2.  xxxx
3.  xxxx

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
