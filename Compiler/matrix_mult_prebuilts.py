import os.path

dirname = os.path.dirname(__file__)
outpath = os.path.join(dirname, 'MatrixMultPrebuilts.cs')

MAX = 4

with open(outpath, 'w') as f:
    f.write('using System;\n')
    f.write('\n')
    f.write('namespace MetaphysicsIndustries.Solus.Compiler;\n')
    f.write('\n')
    f.write('public static class MatrixMultPrebuilts\n')
    f.write('{\n')
    f.write('    // c = a * b\n')
    f.write('    // a is a MxN matrix, b is NxP, c is MxP\n')
    f.write('\n')
    for i in range(MAX):
        for j in range(MAX):
            for k in range(MAX):
                f.write(f'    public static void MatrixMult{i+1}{j+1}{k+1}(float[,] a, float[,] b, float[,] c)\n')
                f.write('    {\n')
                for ii in range(i+1):
                    for kk in range(k+1):
                        f.write(f'        c[{ii},{kk}] = ') 
                        for jj in range(j+1):
                            if jj != 0:
                                f.write(' + ')
                            f.write(f'a[{ii}, {jj}] * b[{jj}, {kk}]')
                        f.write(';\n')
                f.write('    }\n')
                f.write('\n')

    f.write(f'    public static Action<float[,], float[,], float[,]> Get(int m, int n, int p)\n')
    f.write('    {\n')
    f.write(f'        if (m < 1 || m > {MAX}) throw new ArgumentOutOfRangeException(nameof(m));\n')
    f.write(f'        if (n < 1 || n > {MAX}) throw new ArgumentOutOfRangeException(nameof(n));\n')
    f.write(f'        if (p < 1 || p > {MAX}) throw new ArgumentOutOfRangeException(nameof(p));\n')
    f.write('\n')
    f.write('        var funcs = new[]\n')
    f.write('        {\n')
    for m in range(MAX):
        for n in range(MAX):
            f.write(f'           ')
            for p in range(MAX):
                f.write(f' MatrixMult{m+1}{n+1}{p+1},')
            f.write('\n')
    f.write('        };\n')
    f.write(f'        return funcs[{MAX*MAX} * (m-1) + {MAX} * (n-1) + (p-1)];\n')
    f.write('    }\n')

    f.write('}\n')

    f.write('\n')
    f.write('// The following script was used to generate this file:\n')
    f.write('//\n')
    for line in open(__file__):
        f.write(f'// {line}')
