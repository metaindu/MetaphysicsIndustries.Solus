template = 'public delegate TResult CompiledExpression<{}TResult>({});'
for i in range(100):
    x = ['public delegate TResult CompiledExpression<']
    for j in range(i):
        x.append('in T')
        x.append(str(j))
        x.append(', ')
    x.append('out TResult>(')
    for j in range(i):
        x.append('T')
        x.append(str(j))
        x.append(' arg')
        x.append(str(j))
        if j < i - 1:
            x.append(', ')
    x.append(');')
    print(''.join(x))
