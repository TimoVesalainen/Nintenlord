﻿namespace Nintenlord.Grammars.RegularExpression.Tree
{
    /// <summary>
    /// Types or regular expression elements
    /// </summary>
    public enum RegExNodeTypes
    {
        /// <summary>
        /// One letter
        /// </summary>
        Letter,
        /// <summary>
        /// A word with no letters in it
        /// </summary>
        EmptyWord,
        /// <summary>
        /// No words
        /// </summary>
        Empty,
        /// <summary>
        /// All finite repetitions of a word
        /// </summary>
        KleeneClosure,
        /// <summary>
        /// Either first word or second word
        /// </summary>
        Choise,
        /// <summary>
        /// Concatenation of two words
        /// </summary>
        Concatenation,
    }
}
