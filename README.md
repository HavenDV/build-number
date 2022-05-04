# compute-semantic-version
Github Action to compute a current semantic version 
based on the start version and [Conventional Commits](https://www.conventionalcommits.org/en/) specification.

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - name: Compute semantic version
      uses: havendv/compute-semantic-version@master
      with:
        start-version: 1.0.0

    - name: Print semantic version
      run: echo "Semantic version is $SEMANTIC_VERSION"
      # Or, if you're on Windows: echo "Semantic version is ${env:SEMANTIC_VERSION}"
```

After that runs the subsequent steps in your job will have the environment variable `SEMANTIC_VERSION` available. 
If you prefer to be more explicit you can use the output of the step, like so:

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - name: Compute semantic version
      id: compute-semantic-version
      uses: havendv/compute-semantic-version@master
    
    # Now you can pass ${{ steps.compute-semantic-version.outputs.version }} to the next steps.
    - name: Another step as an example
      uses: actions/hello-world-docker-action@v1
      with:
        who-to-greet: ${{ steps.compute-semantic-version.outputs.version }}
```
The `GITHUB_TOKEN` environment variable is defined by GitHub already for you.
See [virtual environments for GitHub actions](https://help.github.com/en/articles/virtual-environments-for-github-actions#github_token-secret) 
for more information.

## Getting the semantic version in other jobs

For other steps in the same job you can use the methods above,
to actually get the semantic version in other jobs you need to use 
[job outputs](https://help.github.com/en/actions/reference/workflow-syntax-for-github-actions#jobsjobs_idoutputs) mechanism:

```yaml
jobs:
  job1:
    runs-on: ubuntu-latest
    outputs:
      version: ${{ steps.compute-semantic-version.outputs.version }}
    steps:
    - name: Compute semantic version
      id: compute-semantic-version
      uses: havendv/compute-semantic-version@master
          
  job2:
    needs: job1
    runs-on: ubuntu-latest
    steps:
    - name: Another step as an example
      uses: actions/hello-world-docker-action@v1
      with:
        who-to-greet: ${{needs.job1.outputs.version}}
```
