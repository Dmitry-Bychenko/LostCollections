using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostCollections.Generic {

  //-------------------------------------------------------------------------------------------------------------------
  //
  /// <summary>
  /// Graph Algorithms
  /// </summary>
  //
  //-------------------------------------------------------------------------------------------------------------------

  public static partial class GraphAlgorithm {
    #region Public

    /// <summary>
    /// Breadth First Search
    /// </summary>
    /// <param name="roots">Roots to start with</param>
    /// <param name="edges">Edges from node</param>
    /// <returns>Enumerated nodes</returns>
    /// <exception cref="ArgumentNullException">When roots or edges are null</exception>
    public static IEnumerable<T> BreadthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> edges) {
      if (roots is null)
        throw new ArgumentNullException(nameof(roots));
      if (edges is null)
        throw new ArgumentNullException(nameof(edges));

      HashSet<T> completed = new HashSet<T>();
      Queue<T> agenda = new Queue<T>(roots);

      while (agenda.Count > 0) 
        for (int i = agenda.Count - 1; i >= 0; --i) {
          T node = agenda.Dequeue();
          
          if (!completed.Add(node)) {
            yield return node;

            foreach (var next in edges(node))
              if (!completed.Contains(next))
                agenda.Enqueue(next);
          }
        }
    }

    /// <summary>
    /// Depth First Search
    /// </summary>
    /// <param name="roots">Roots to start with</param>
    /// <param name="edges">Edges from node</param>
    /// <returns>Enumerated nodes</returns>
    /// <exception cref="ArgumentNullException">When roots or edges are null</exception>
    public static IEnumerable<T> DepthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> edges) {
      if (roots is null)
        throw new ArgumentNullException(nameof(roots));
      if (edges is null)
        throw new ArgumentNullException(nameof(edges));

      HashSet<T> completed = new HashSet<T>();
      Stack<T> agenda = new Stack<T>(roots);

      while (agenda.Count > 0) {
        T node = agenda.Pop();

        if (!completed.Add(node)) {
          yield return node;

          foreach (var next in edges(node))
            if (!completed.Contains(next))
              agenda.Push(next);
        }
      }
    }

    #endregion Public
  }

}
