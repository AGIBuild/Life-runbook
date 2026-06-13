# 人生哨兵 / LifeRunbook AI 开发交付说明

## 1. 开发目标

请开发一个 MVP 版本的 **人生哨兵 / LifeRunbook**。

MVP 目标不是做完整人生管理平台，而是验证：

1. 用户是否愿意创建真实关键事件预案。
2. 用户是否愿意添加真实紧急联系人。
3. 用户是否愿意为多预案、加密备份、短信通知和 AI 预案生成付费。

第一版聚焦：

- 失联预案
- 独自出行预案
- 数字资产预案
- 紧急联系人
- 定时确认
- 超时通知
- AI 预案生成

## 2. 推荐技术栈

### 2.1 客户端

优先方案：

- iOS: Swift + SwiftUI

可选方案：

- Flutter
- React Native

如果为了快速验证，可以先做 Web App + PWA，但长期产品更适合原生移动端，因为涉及通知、位置、联系人和后台任务。

### 2.2 后端

推荐：

- .NET 8/9 Web API
- PostgreSQL
- Redis
- Background Worker
- Object Storage
- AI Provider Adapter
- SMS/Email Provider Adapter

### 2.3 基础服务

- Authentication
- Runbook Management
- Trusted Contact Management
- Trigger Engine
- Notification Service
- AI Planning Service
- Audit Log Service
- Secure Vault Service

## 3. 模块边界

### 3.1 Auth Service

职责：

- 用户注册
- 登录
- Token 签发
- 账号注销

第一版支持：

- Email 登录
- 手机号登录可后置

### 3.2 Trusted Contact Service

职责：

- 添加紧急联系人
- 邀请联系人确认
- 设置联系人权限
- 维护备用联系方式

联系人权限：

- NotifyOnly: 只接收通知
- EmergencyCard: 可查看应急卡片
- RunbookViewer: 可查看指定预案
- SensitiveAccess: 触发条件满足后可查看指定敏感资料

### 3.3 Runbook Service

职责：

- 创建预案
- 编辑预案
- 启用/暂停预案
- 手动启动预案
- 手动解除预案
- 预案模板管理

MVP 预案类型：

- MissingCheckIn
- SoloTrip
- DigitalAsset

### 3.4 Trigger Engine

职责：

- 定时检查预案触发条件
- 生成确认请求
- 判断超时
- 触发通知动作
- 记录触发日志

MVP 触发器：

- ScheduledCheckIn
- DeadlineNoResponse
- ManualStart
- ManualCancel

不要在第一版实现复杂自动推断。

### 3.5 Notification Service

职责：

- 发送 App Push
- 发送 Email
- 发送 SMS
- 记录发送状态
- 失败重试

MVP 通知动作：

- NotifyOwner
- NotifyTrustedContacts
- ShareLastKnownLocation
- SendEmergencyCard

### 3.6 AI Planning Service

职责：

- 根据自然语言生成预案草稿
- 检查预案缺口
- 生成联系人通知文案
- 将用户描述转为结构化规则

要求：

- AI 输出必须结构化 JSON。
- AI 生成内容必须进入用户确认页面。
- AI 不允许直接启用高风险触发动作。

### 3.7 Secure Vault Service

职责：

- 存储用户敏感资料
- 字段级加密
- 权限检查
- 访问日志

MVP 可先实现基础加密字段，不需要完整端到端加密，但接口设计必须为后续 E2EE 预留。

### 3.8 Audit Log Service

职责：

- 记录关键操作
- 支持用户查看触发历史
- 支持排查误触发

必须记录：

- RunbookCreated
- RunbookUpdated
- RunbookActivated
- RunbookPaused
- CheckInRequested
- CheckInConfirmed
- TriggerFired
- NotificationSent
- ContactViewedEmergencyCard
- PermissionChanged

## 4. 核心数据模型

### 4.1 User

字段：

- Id
- Email
- PhoneNumber
- DisplayName
- CreatedAt
- UpdatedAt
- DeletedAt

### 4.2 TrustedContact

字段：

- Id
- OwnerUserId
- Name
- Email
- PhoneNumber
- Relationship
- Status
- PermissionLevel
- CreatedAt
- UpdatedAt

Status:

- Pending
- Accepted
- Revoked

### 4.3 Runbook

字段：

- Id
- UserId
- Type
- Title
- Description
- Status
- TemplateVersion
- CreatedAt
- UpdatedAt
- ActivatedAt
- PausedAt

Type:

- MissingCheckIn
- SoloTrip
- DigitalAsset
- FamilyEmergency
- JobTransition

Status:

- Draft
- Active
- Paused
- Triggered
- Archived

### 4.4 RunbookStep

字段：

- Id
- RunbookId
- Title
- Description
- SortOrder
- IsSensitive
- CreatedAt
- UpdatedAt

### 4.5 TriggerRule

字段：

- Id
- RunbookId
- Type
- ConfigJson
- Status
- CreatedAt
- UpdatedAt

Type:

- ScheduledCheckIn
- DeadlineNoResponse
- ManualStart
- ManualCancel

### 4.6 CheckInSession

字段：

- Id
- RunbookId
- UserId
- RequestedAt
- ExpiresAt
- ConfirmedAt
- Status

Status:

- Pending
- Confirmed
- Expired
- Escalated
- Cancelled

### 4.7 Notification

字段：

- Id
- RunbookId
- RecipientType
- RecipientContactId
- Channel
- Subject
- Body
- Status
- SentAt
- FailedAt
- ErrorMessage

Channel:

- Push
- Email
- SMS

Status:

- Pending
- Sent
- Failed
- Retrying

### 4.8 EmergencyCard

字段：

- Id
- RunbookId
- Summary
- LastKnownLocationJson
- VisibleFieldsJson
- CreatedAt
- UpdatedAt

### 4.9 SecureItem

字段：

- Id
- UserId
- RunbookId
- Category
- Title
- EncryptedContent
- SensitivityLevel
- CreatedAt
- UpdatedAt

Category:

- PasswordManager
- Domain
- Server
- CloudAccount
- Wallet
- Insurance
- Document
- Other

### 4.10 AuditLog

字段：

- Id
- UserId
- EntityType
- EntityId
- Action
- MetadataJson
- CreatedAt

## 5. 关键用户流程

### 5.1 创建失联预案

1. 用户选择“失联预案”模板。
2. 输入确认周期、宽限时间、通知联系人。
3. 输入通知内容。
4. 系统展示触发流程预览。
5. 用户确认启用。
6. 到达时间后系统推送确认请求。
7. 用户未确认则进入超时流程。
8. 系统通知联系人并记录日志。

### 5.2 创建独自出行预案

1. 用户选择“独自出行预案”。
2. 输入出发时间、预计结束时间、目的地、交通方式。
3. 添加联系人。
4. 可选填写车牌、酒店、保险、同行说明。
5. 启用预案。
6. 到点未确认则通知联系人。

### 5.3 创建数字资产预案

1. 用户选择“数字资产预案”。
2. 添加资产类别。
3. 填写获取路径和处理说明。
4. 设置哪些联系人在什么条件下可见。
5. 系统加密保存。
6. 用户可定期复查。

### 5.4 AI 生成预案

1. 用户输入自然语言描述。
2. AI 返回结构化预案草稿。
3. 系统展示缺失字段。
4. 用户补充信息。
5. 用户确认启用。

## 6. AI Prompt 要求

AI 输出必须遵循 JSON Schema。

示例输出结构：

```json
{
  "title": "甘南自驾失联预案",
  "type": "SoloTrip",
  "summary": "用户独自前往甘南自驾，超过确认时间未响应时通知联系人。",
  "trigger": {
    "type": "DeadlineNoResponse",
    "checkInTime": "2026-06-15T22:00:00+08:00",
    "gracePeriodHours": 8
  },
  "contacts": [
    {
      "relationship": "spouse",
      "permissionLevel": "EmergencyCard"
    }
  ],
  "emergencyMessage": "我可能暂时无法联系，请先尝试电话联系我，并查看最后位置、车牌和酒店信息。",
  "missingFields": [
    "车牌号",
    "酒店地址",
    "保险联系方式"
  ],
  "riskNotes": [
    "请确认联系人手机号有效。",
    "请在出发前手动启用该预案。"
  ]
}
```

约束：

- 不生成医疗诊断。
- 不生成法律结论。
- 不承诺救援结果。
- 不自动启用预案。
- 不直接向联系人发送未确认内容。

## 7. API 草案

### 7.1 Runbooks

- `POST /api/runbooks`
- `GET /api/runbooks`
- `GET /api/runbooks/{id}`
- `PUT /api/runbooks/{id}`
- `POST /api/runbooks/{id}/activate`
- `POST /api/runbooks/{id}/pause`
- `POST /api/runbooks/{id}/manual-trigger`
- `POST /api/runbooks/{id}/cancel-trigger`

### 7.2 Contacts

- `POST /api/contacts`
- `GET /api/contacts`
- `PUT /api/contacts/{id}`
- `DELETE /api/contacts/{id}`
- `POST /api/contacts/{id}/invite`
- `POST /api/contact-invitations/{token}/accept`

### 7.3 Check-ins

- `POST /api/checkins/{id}/confirm`
- `POST /api/checkins/{id}/snooze`
- `GET /api/checkins/pending`

### 7.4 AI

- `POST /api/ai/runbook-draft`
- `POST /api/ai/runbook-gap-check`
- `POST /api/ai/emergency-message`

### 7.5 Secure Items

- `POST /api/secure-items`
- `GET /api/secure-items`
- `GET /api/secure-items/{id}`
- `PUT /api/secure-items/{id}`
- `DELETE /api/secure-items/{id}`

### 7.6 Audit Logs

- `GET /api/audit-logs`
- `GET /api/runbooks/{id}/audit-logs`

## 8. 14 天开发计划

### Day 1-2: 项目骨架

- 创建后端 API 项目
- 创建数据库迁移
- 实现用户模型
- 实现基础认证
- 创建前端项目骨架

### Day 3-4: 联系人与预案

- 实现 TrustedContact CRUD
- 实现 Runbook CRUD
- 实现失联预案模板
- 实现预案启用/暂停

### Day 5-6: Check-in 与 Trigger

- 实现 CheckInSession
- 实现后台定时任务
- 实现超时判断
- 实现手动确认
- 实现触发日志

### Day 7-8: 通知

- 实现 Email 通知
- 实现 SMS Provider 抽象
- 实现通知状态记录
- 实现失败重试

### Day 9-10: 数字资产预案

- 实现 SecureItem
- 实现字段加密
- 实现基础权限判断
- 实现数字资产预案 UI

### Day 11-12: AI 预案生成

- 实现 AI Provider Adapter
- 实现自然语言转预案草稿
- 实现缺口检查
- 实现用户确认页

### Day 13: 落地页与付费页

- 首页
- 价格页
- Pro 版权益页
- 等待名单或人工支付入口

### Day 14: 测试与演示

- 完成核心流程测试
- 准备 3 个演示账号
- 准备演示数据
- 输出已知限制清单

## 9. 验收标准

MVP 必须能完成：

1. 用户注册登录。
2. 添加紧急联系人。
3. 创建失联预案。
4. 启用失联预案。
5. 到点生成确认请求。
6. 用户未确认后触发通知。
7. 联系人收到通知。
8. 系统记录完整日志。
9. 用户创建数字资产预案。
10. AI 根据自然语言生成预案草稿。

不要求完成：

- 原生 App Store 上架
- 微信通知
- 端到端加密完整实现
- 电话呼叫
- 地理围栏
- 自动健康监测
- 自动离职判断

## 10. 安全和法律边界

所有界面必须明确：

- 本产品不是救援服务。
- 本产品不是医疗服务。
- 本产品不是法律服务。
- 通知成功受网络、手机权限、短信服务商等因素影响。
- 用户需要自行确认联系人信息有效。

敏感信息处理要求：

- 不记录明文密码。
- 不展示未授权敏感信息。
- 不把用户隐私内容用于模型训练。
- 不默认上传本地敏感资料。

## 11. 推荐开发顺序

如果时间不足，按以下顺序砍功能：

1. 保留失联预案。
2. 保留联系人通知。
3. 保留触发日志。
4. 保留 AI 预案草稿。
5. 砍数字资产预案的复杂权限。
6. 砍短信，仅保留邮件。
7. 砍云同步。
8. 砍地理围栏。

最小可演示闭环：

> 创建失联预案 -> 设置联系人 -> 定时确认 -> 超时未响应 -> 通知联系人 -> 记录日志。
