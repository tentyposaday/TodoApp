#!/usr/bin/env bash

set -euo pipefail

ROOT_NAMESPACE="TodoApp"

NAMESPACES=(
    "Data"
    "Dtos"
    "Interfaces"
    "Models"
    "Repositories"
    "Services"
)

find . -type f -name "*.cs" \
    -not -path "./bin/*" \
    -not -path "./obj/*" \
    -print0 |
while IFS= read -r -d '' file; do
    for namespace in "${NAMESPACES[@]}"; do
        # namespace Foo;
        sed -i \
            "s/^namespace ${namespace};$/namespace ${ROOT_NAMESPACE}.${namespace};/" \
            "$file"

        # namespace Foo { ... }
        sed -i \
            "s/^namespace ${namespace}$/namespace ${ROOT_NAMESPACE}.${namespace}/" \
            "$file"

        # using Foo;
        sed -i \
            "s/^using ${namespace};$/using ${ROOT_NAMESPACE}.${namespace};/" \
            "$file"
    done
done

echo "Namespaces and usings updated."
