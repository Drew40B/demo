#!/usr/bin/env python3
from pathlib import Path


def find_common_root() -> Path:
    path = Path.cwd()

    test_path = path.joinpath("rootCommon")
    if test_path.exists():
        return test_path

    for parent in path.parents:
        test_path = parent.joinpath("rootCommon")
        if test_path.exists():
            return test_path

    raise Exception("Unable to find root common")
