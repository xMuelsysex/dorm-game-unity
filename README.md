# 宿舍游戏 (Dorm Game)

崩坏三风格的 Unity 宿舍互动游戏

## 项目概述

这是一个使用 Unity 2022.3 LTS 开发的 2D/2.5D 宿舍场景游戏，包含角色对话系统、场景交互和探索功能。

## 技术栈

- **Unity**: 2022.3.62f3 LTS
- **渲染管线**: Universal Render Pipeline (URP)
- **UI 系统**: Unity UGUI
- **文本渲染**: TextMeshPro 3.0.6
- **输入系统**: Input System 1.7.0

## 已实现功能

### 阶段 1：核心移动和交互系统
- ✅ WASD 角色移动
- ✅ 碰撞检测
- ✅ NPC 交互触发（按 E 键）

### 阶段 2：对话系统骨架
- ✅ 对话数据结构（DialogueNode, DialogueChoice）
- ✅ 对话 UI（打字机效果、选项按钮）
- ✅ 对话触发器（DialogueTrigger）
- ✅ 中文字体系统（Static + Dynamic Fallback）
- ✅ 响应式对话框布局（占屏幕 25% 高度）

## 项目结构

```
Assets/_Project/
├── Scenes/              # 场景文件
│   └── TestMovement.unity
├── Scripts/             # C# 脚本
│   ├── Player/          # 玩家控制
│   ├── NPC/             # NPC 交互
│   ├── Dialogue/        # 对话系统
│   ├── Core/            # 核心系统
│   └── Data/            # 数据定义
├── Data/                # ScriptableObject 数据
│   ├── Characters/      # 角色配置
│   └── Dialogues/       # 对话配置
├── Prefabs/             # 预制体
│   └── UI/              # UI 预制体
└── Fonts/               # 字体资源
    ├── simhei SDF HQ.asset      # 主字体（Static）
    └── simhei SDF Dynamic.asset  # 备用字体（Dynamic）
```

## 开发计划

- [ ] **阶段 3**: 2.5D 宿舍场景系统
- [ ] **阶段 4**: 角色动画和状态机
- [ ] **阶段 5**: 场景特效和光照

## 开发环境

### 必需软件
- Unity 2022.3.62f3 LTS
- Visual Studio 2022 或 JetBrains Rider

### 推荐配置
- Windows 10/11
- .NET Framework 4.8+

## 如何运行

1. 克隆仓库
2. 使用 Unity Hub 打开项目
3. 打开场景：`Assets/_Project/Scenes/TestMovement.unity`
4. 点击 Play 按钮

## 操作说明

- **WASD**: 移动角色
- **E**: 与 NPC 交互
- **鼠标左键**: 点击对话框"继续"按钮或选择选项

## 贡献指南

本项目使用 Git Flow 工作流：
- `main` 分支：稳定版本
- `develop` 分支：开发版本
- `feature/*` 分支：新功能开发

## 许可证

MIT License

## 联系方式

- 开发者：Muelsyse-Elysia
- 仓库：https://github.com/Muelsyse-Elysia/dorm-game-unity
