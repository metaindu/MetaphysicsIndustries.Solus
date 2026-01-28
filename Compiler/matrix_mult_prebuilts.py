import os.path

dirname = os.path.dirname(__file__)
outpath = os.path.join(dirname, 'MatrixMultPrebuilts.cs')

N = 4

with open(outpath, 'w') as f:
    f.write('public static class MatrixMultPrebuilts\n{')
    f.write('\n')
    for i in range(N):
        for j in range(N):
            for k in range(N):
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
    f.write('}\n')

    f.write('// The following script was used to generate this file:\n')
    f.write('//\n')
    for line in open(__file__):
        f.write(f'// {line}')
