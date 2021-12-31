# 1.0.0-alpha.12

- Updated Statiq Web to version [1.0.0-beta.37](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.38).
- Updated Statiq Framework to version [1.0.0-beta.52](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.53).
- Set the `Xref` setting for all API documents to be prefixed by "api-" to avoid collisions with site content.

# 1.0.0-alpha.11

- Updated Statiq Web to version [1.0.0-beta.37](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.37).
- Updated Statiq Framework to version [1.0.0-beta.52](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.52).
- 
# 1.0.0-alpha.10

- Updated Statiq Web to version [1.0.0-beta.36](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.36).
- Updated Statiq Framework to version [1.0.0-beta.51](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.51).

# 1.0.0-alpha.9

- Updated Statiq Web to version [1.0.0-beta.35](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.35).
- Updated Statiq Framework to version [1.0.0-beta.50](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.50).

# 1.0.0-alpha.8

- Updated Statiq Web to version [1.0.0-beta.34](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.34).
- Updated Statiq Framework to version [1.0.0-beta.49](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.49).
- Added `DocsKeys.OutputApiDocuments` to control whether the documents representing code analysis symbols should be output to the Statiq Web Content pipeline.
- Added `DocsKeys.ApiLayout` to specify a special layout for use with code analysis symbol documents (useful for themes to provide a layout at a consistent location).
- Made the `Api` pipeline a dependency of the Statiq Web Content pipeline and added a content type so that the output documents are processed when `DocsKeys.OutputApiDocuments` is set to `true`.S

# 1.0.0-alpha.7

- Updated Statiq Web to version [1.0.0-beta.32](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.32).
- Updated Statiq Framework to version [1.0.0-beta.47](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.47).

# 1.0.0-alpha.6

- Updated Statiq Web to version [1.0.0-beta.31](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.31).
- Updated Statiq Framework to version [1.0.0-beta.46](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.46).

# 1.0.0-alpha.5

- Updated Statiq Web to version [1.0.0-beta.30](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.30).
- Updated Statiq Framework to version [1.0.0-beta.44](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.44).

# 1.0.0-alpha.4

- Updated Statiq Web to version [1.0.0-beta.29](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.29).
- Updated Statiq Framework to version [1.0.0-beta.43](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.43).

# 1.0.0-alpha.3

- Updated Statiq Web to version [1.0.0-beta.29](https://github.com/statiqdev/Statiq.Web/releases/tag/v1.0.0-beta.29).
- Updated Statiq Framework to version [1.0.0-beta.43](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.43).

# 1.0.0-alpha.2

- Ensures the Api pipeline is a dependency of the Statiq Web Inputs pipeline.
- Added a null check to the `AssemblyFiles`, `ProjectFiles`, and `SolutionFiles` settings (#44, thanks @bjorkstromm).
- Added the `CacheDocuments` module to cache code analysis symbol documents (#45, thanks @bjorkstromm).
- Updated Statiq Framework to version [1.0.0-beta.42](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.42).

# 1.0.0-alpha.1

- Initial version (thanks for getting it started @bjorkstromm!).