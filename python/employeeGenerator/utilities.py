#!/usr/bin/env python3
from pathlib import Path

def findCommonRoot() -> Path:
    path = Path.cwd()

    testPath = path.joinpath("rootCommon")
    if testPath.exists():
        return testPath

    for parent in path.parents:
        testPath = parent.joinpath("rootCommon")
        if testPath.exists():
            return testPath

    raise Exception("Unable to find root common")