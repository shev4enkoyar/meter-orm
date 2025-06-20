# Contributing to MeterOrm

Thank you for your interest in contributing to MeterOrm! This document provides guidelines and information for contributors.

## 🤝 How to Contribute

### Reporting Bugs

- Use the [Bug Report](https://github.com/shev4enkoyar/meter-orm/issues/new?template=bug_report.md) template
- Include detailed steps to reproduce the issue
- Provide system information and error logs
- Check if the issue has already been reported

### Suggesting Enhancements

- Use the [Feature Request](https://github.com/shev4enkoyar/meter-orm/issues/new?template=feature_request.md) template
- Describe the problem you're trying to solve
- Explain why this enhancement would be useful
- Provide examples of how it would work

### Code Contributions

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Make your changes**
4. **Add tests** for new functionality
5. **Ensure all tests pass**: `dotnet test`
6. **Commit your changes**: `git commit -m 'Add amazing feature'`
7. **Push to the branch**: `git push origin feature/amazing-feature`
8. **Open a Pull Request**

## 🛠 Development Setup

### Prerequisites

- .NET 9.0 SDK or later
- Git
- Your preferred IDE (Visual Studio, VS Code, Rider, etc.)

### Getting Started

1. **Clone your fork**:

   ```bash
   git clone https://github.com/YOUR_USERNAME/meter-orm.git
   cd meter-orm
   ```

2. **Add the upstream repository**:

   ```bash
   git remote add upstream https://github.com/shev4enkoyar/meter-orm.git
   ```

3. **Restore dependencies**:

   ```bash
   dotnet restore
   ```

4. **Build the solution**:

   ```bash
   dotnet build
   ```

5. **Run tests**:

   ```bash
   dotnet test
   ```

## 📝 Coding Standards

### Code Style

- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods focused and concise
- Use async/await for I/O operations

### Commit Messages

Use conventional commit format:

```git
type(scope): description

[optional body]

[optional footer]
```

Types:

- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes
- `refactor`: Code refactoring
- `test`: Test changes
- `chore`: Build/tooling changes

Examples:

```git
feat(dlms): added support for extended register objects
fix(core): resolved memory leak in meter context disposal
docs(readme): updated installation instructions
```

### Pull Request Guidelines

- **Title**: Clear and descriptive
- **Description**: Explain what and why, not how
- **Tests**: Include tests for new functionality
- **Documentation**: Update docs if needed
- **Breaking Changes**: Clearly mark and explain

## 🧪 Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test tests/MeterOrm.Core.Tests/
```

### Writing Tests

- Use xUnit as the testing framework
- Follow AAA pattern (Arrange, Act, Assert)
- Test both positive and negative scenarios
- Mock external dependencies
- Use descriptive test names

Example:

```csharp
[Fact]
public async Task GetMeterById_WithValidId_ReturnsMeter()
{
    // Arrange
    var context = CreateTestContext();
    var expectedMeter = CreateTestMeter();

    // Act
    var result = await context.Set<ElectricityMeter>()
        .FirstOrDefaultAsync(m => m.Id == expectedMeter.Id);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedMeter.Id, result.Id);
}
```

## 📚 Documentation

### Code Documentation

- Add XML comments for all public APIs
- Include parameter descriptions and return values
- Provide usage examples for complex methods

### User Documentation

- Update README.md for user-facing changes
- Add examples in the docs/ folder
- Update API reference documentation

## 🔍 Review Process

1. **Automated Checks**: All PRs must pass CI checks
2. **Code Review**: At least one maintainer must approve
3. **Testing**: Ensure all tests pass
4. **Documentation**: Verify documentation is updated

## 🏷 Release Process

1. **Version Bump**: Update version numbers
2. **Changelog**: Update CHANGELOG.md
3. **Tag Release**: Create git tag
4. **Publish**: Release to NuGet

## 🆘 Getting Help

- **Issues**: Use GitHub issues for bugs and feature requests
- **Discussions**: Use GitHub Discussions for questions
- **Security**: Report security issues via SECURITY.md

## 📄 License

By contributing to MeterOrm, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing to MeterOrm! 🎉
