using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostCollections.Generic {

  //-------------------------------------------------------------------------------------------------------------------
  //
  /// <summary>
  /// 
  /// </summary>
  //
  //-------------------------------------------------------------------------------------------------------------------

  public static class GraphSearch {
    #region Public

    /// <summary>
    /// Breadth Firth Search (BFS)
    /// </summary>
    /// <param name="root">Root</param>
    /// <param name="nodes">Nodes</param>
    /// <returns>Enumerated nodes</returns>
    /// <exception cref="ArgumentNullException">When nodes is null</exception>
    public static IEnumerable<T> BreadthFirth<T>(T root, Func<T, IEnumerable<T>> nodes) {
      if (nodes is null)
        throw new ArgumentNullException(nameof(nodes));

      HashSet<T> completed = new HashSet<T>();
      Queue<T> agenda = new Queue<T>();

      agenda.Enqueue(root);

      while (agenda.Count > 0) 
        for (int i = agenda.Count - 1; i >= 0; --i) {
          T node = agenda.Dequeue();

          if (completed.Add(node)) {
            yield return node;

            foreach (T next in nodes(node)) 
              if (!completed.Contains(next))
                agenda.Enqueue(next);
          }
        }
    }

    /// <summary>
    /// Depth Firth Search (DFS)
    /// </summary>
    /// <param name="root">Root</param>
    /// <param name="nodes">Nodes</param>
    /// <returns>Enumerated nodes</returns>
    /// <exception cref="ArgumentNullException">When nodes is null</exception>
    public static IEnumerable<T> DepthFirth<T>(T root, Func<T, IEnumerable<T>> nodes) {
      if (nodes is null)
        throw new ArgumentNullException(nameof(nodes));

      HashSet<T> completed = new HashSet<T>();
      Stack<T> agenda = new Stack<T>();

      agenda.Push(root);

      while (agenda.Count > 0) {
        T node = agenda.Pop();

        if (completed.Add(node)) {
          yield return node;

          foreach (T next in nodes(node))
            if (!completed.Contains(next))
              agenda.Push(next);
        }
      }
    }

    #endregion Public
  }

}
