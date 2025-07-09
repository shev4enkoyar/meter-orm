---
name: Feature request
about: Suggest an idea for this project
title: '[FEATURE] '
labels: ['enhancement', 'needs-triage']
assignees: ''
---

## 🚀 Feature Description

A clear and concise description of the feature you'd like to see.

## 🎯 Problem Statement

A clear and concise description of what problem this feature would solve. For example:

- "I'm always frustrated when [...]"
- "It would be helpful if [...]"
- "Currently, there's no way to [...]"

## 💡 Proposed Solution

A clear and concise description of what you want to happen.

## 🔄 Alternative Solutions

A clear and concise description of any alternative solutions or features you've considered.

## 📋 Use Cases

Describe specific scenarios where this feature would be useful:

1. **Use Case 1**: [Description]
2. **Use Case 2**: [Description]
3. **Use Case 3**: [Description]

## 💻 Code Example

If applicable, provide a code example of how this feature might work:

```csharp
// Example usage
using var context = new DlmsMeterContext(connection);
var meters = await context.Set<ElectricityMeter>()
    .Where(m => m.Consumption > 1000)
    .OrderByDescending(m => m.LastReading)
    .Take(10)
    .ToListAsync();
```

## 🔧 Technical Considerations

- **Performance Impact**: Will this affect performance?
- **Breaking Changes**: Does this require breaking changes?
- **Dependencies**: Are there any new dependencies required?
- **Backward Compatibility**: How does this affect existing code?

## 📊 Priority

How important is this feature to you?

- [ ] **Critical** - Blocking my work
- [ ] **High** - Important for my use case
- [ ] **Medium** - Nice to have
- [ ] **Low** - Would be cool

## 🔍 Research

- [ ] I have searched existing issues to avoid duplicates
- [ ] I have checked the documentation for existing solutions
- [ ] I have researched similar features in other libraries

## 📋 Additional Context

Add any other context, screenshots, or examples about the feature request here.

## 🤝 Contributing

Would you be interested in contributing to the implementation of this feature?

- [ ] Yes, I can help implement this
- [ ] Yes, I can help with testing
- [ ] Yes, I can help with documentation
- [ ] No, but I can provide feedback
- [ ] No, I just want to suggest the feature

---

**Thank you for your feature request!** 🎉